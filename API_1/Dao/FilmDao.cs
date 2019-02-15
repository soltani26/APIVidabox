using API_1.Models;
using API_1.Utils_Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_1.Dao
{
    public class FilmDAO
    {
        /// <summary>
        /// Met à jour la liste des séries
        /// </summary>
        /// <returns></returns>
        public static List<Film> upDateListFilms()
        {
            string req = "select * from Films";
            var results = DataBase.select(req, new Dictionary<string, string>());

            List<Film> listeFilms = new List<Film>();

            foreach (var item in results)
            {
                var id = Convert.ToInt32(item["Id_Films"]);
                var titre = item["Titre"];
                var synopsis = item["synopsis"];
                var dateSortie = item["dateSortie"];
                var duree = Convert.ToInt32(item["duree"]);
                var urlAffiche = item["Url_Affiche"];
                var urlBandeAnnonce = item["url_BandeAnnoce"];
                int idGenre = Convert.ToInt32(item["Id_genre"]);
                int idDbMovie = Convert.ToInt32(item["id_moviedb"]);


                listeFilms.Add(new Film(id, titre, synopsis, dateSortie, duree, urlAffiche, urlBandeAnnonce, idGenre, idDbMovie));
            }

            return listeFilms;
        }

        /// <summary>
        /// Ajoute l'ensemble des données des films depuis l'api distante vers l'api locale
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        public static void GetResultAll(string query, int page)
        {
            // on appelle l'api distante pour récupérer les résultats de la recherche
            string urlMovie = "https://api.themoviedb.org/3/search/movie?api_key=a77cba9e9e7532eeccc7a2a0239bb7ff&query=" + query + "&page=" + page + "&language=fr-FR";
            

            string jsonResult = MovieDbAPI_Helper.Call_MovieDbAPI(query, page, urlMovie);
            var items = JsonConvert.DeserializeObject<Film.Search.RootObject>(jsonResult);
            var results = items.results;
            // on ajoute tous les résultats à notre bdd
            foreach (var item in results)
            {
                if (item.original_language == "fr" || item.original_language == "en")
                {
                    string urlMovieDetails = "https://api.themoviedb.org/3/movie/" + item.id + "?api_key=a77cba9e9e7532eeccc7a2a0239bb7ff&language=fr-FR";
                    // on récupère les details des séries
                    string jsonDetails = MovieDbAPI_Helper.Call_Details_MovieDbAPI(urlMovieDetails);
                    var itemsDetails = JsonConvert.DeserializeObject<Film.Details.RootObject>(jsonDetails);

                    // on enregistre dans les variables les valeurs de chaque champs
                    var titre = item.original_title;
                    var synopsis = item.overview;
                    var date_sortie = itemsDetails.release_date;
                    if (date_sortie == null || date_sortie.Count() < 4)
                        date_sortie = "2000-01-01";
                    var url_affiche = itemsDetails.poster_path;
                    var duree = itemsDetails.runtime.ToString();
                    if (url_affiche == null)
                        url_affiche = "";
                    var url_bande_annonce = string.Empty;
                    string id_genre = string.Empty;
                    if (item.genre_ids.Count == 0)
                        id_genre = "-1";
                    else
                        id_genre = item.genre_ids[0].ToString();
                    var id_moviedb = item.id;

                    // on ajoute à la base de données tous les éléments

                  
                    DataBase.AddFilms(titre, synopsis, date_sortie, duree, url_affiche, url_bande_annonce, id_genre, id_moviedb);
                    
                }
            }
        }


        /// <summary>
        /// Met à jour les données de l'api locale à partir des résultats de l'api distante
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        public static void getResultUpdate(string query, int page)
        {
            
            var listeFilm = FilmDAO.upDateListFilms().Where(films => films.Titre.ToLower().Contains(query.ToLower()));
            // on crée une liste contenant les titres des séries présent dans notre api locale
            List<string> listeId_ApiLocale = new List<string>();

            foreach (var item in listeFilm)
            {
                listeId_ApiLocale.Add(item.Id_dbmovie.ToString());
            }

            string urlMovie = "https://api.themoviedb.org/3/search/movie?api_key=a77cba9e9e7532eeccc7a2a0239bb7ff&query=" + query + "&page=" + page + "&language=fr-FR";
            // on crée une liste contenant les titre des séries présent dans l'api distante
            string jsonResult = MovieDbAPI_Helper.Call_MovieDbAPI(query, page, urlMovie);
            var items = JsonConvert.DeserializeObject<Film.Search.RootObject>(jsonResult);
            var listeFilmDistant = items.results;

            foreach (var item in listeFilmDistant)
            {
                if (item.original_language == "fr" || item.original_language == "en")
                {
                    if (!listeId_ApiLocale.Contains(item.id.ToString()))
                    {
                        string urlMovieDetails = "https://api.themoviedb.org/3/movie/" + item.id + "?api_key=a77cba9e9e7532eeccc7a2a0239bb7ff&language=fr-FR";
                        string jsonDetails = MovieDbAPI_Helper.Call_Details_MovieDbAPI(urlMovieDetails);
                        var itemsDetails = JsonConvert.DeserializeObject<Film.Details.RootObject>(jsonDetails);

                        // on enregistre dans les variables les valeurs de chaque champs
                        var titre = item.original_title;
                        var synopsis = item.overview;
                        var date_sortie = itemsDetails.release_date;
                        if (date_sortie == null || date_sortie.Count() < 4)
                            date_sortie = "2000-01-01";
                        var url_affiche = itemsDetails.poster_path;
                        var duree = itemsDetails.runtime.ToString();
                        if (url_affiche == null)
                            url_affiche = "";
                        var url_bande_annonce = string.Empty;
                        string id_genre = string.Empty;
                        if (item.genre_ids.Count == 0)
                            id_genre = "-1";
                        else
                            id_genre = item.genre_ids[0].ToString();
                        var id_moviedb = item.id;

                        DataBase.AddFilms(titre, synopsis, date_sortie, duree, url_affiche, url_bande_annonce, id_genre, id_moviedb);
                    }
                }
            }
        }

        /// <summary>
        /// retourne la liste des films depuis l'api locale ou l'api distante
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IEnumerable<Film> GetResults(string query, int page)
        {
            var listeFilms = FilmDAO.upDateListFilms().Where(films => films.Titre.ToLower().Contains(query.ToLower()));

            if (listeFilms.Count() == 0)
            {
                GetResultAll(query, page);
                listeFilms = FilmDAO.upDateListFilms().Where(films => films.Titre.ToLower().Contains(query.ToLower()));
                return listeFilms;
            }

            else
            {
                getResultUpdate(query, page);
                listeFilms = FilmDAO.upDateListFilms().Where(series => series.Titre.ToLower().Contains(query.ToLower()));
                return listeFilms;
            }
        }


    }
}
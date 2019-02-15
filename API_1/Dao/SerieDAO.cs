using API_1.Models;
using API_1.Utils_Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_1.Dao
{
    public class SerieDAO
    {

        /// <summary>
        /// Met à jour la liste des séries
        /// </summary>
        /// <returns></returns>
        public static List<Serie> upDateListSerie()
        {
            string req = "select * from Serie";
            var results = DataBase.select(req, new Dictionary<string, string>());

            List<Serie> listeSeries = new List<Serie>();
          
            foreach (var item in results)
            {
                var id = Convert.ToInt32(item["Id_Serie"]);
                var titre = item["Titre"];
                var synopsis = item["Synopsis"];
                var dateSortie = item["Date_sortie"];
                var urlAffiche = item["Url_affiche"];
                var urlBandeAnnonce = item["Url_bande_annonce"];
                int idGenre = Convert.ToInt32(item["Id_genre"]);
                int idDbMovie = Convert.ToInt32(item["id_moviedb"]);

                listeSeries.Add(new Serie(id, titre, synopsis, dateSortie, urlAffiche, urlBandeAnnonce, idGenre, idDbMovie));
            }

            return listeSeries;
        }

        /// <summary>
        /// Ajoute l'ensemble des données des séries depuis l'api distante vers l'api locale
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        public static void GetResultAll(string query, int page)
        {
            string UrlSerie = "https://api.themoviedb.org/3/search/tv?api_key=a77cba9e9e7532eeccc7a2a0239bb7ff&query=" + query + "&page=" + page + "&language=fr-FR";
            // on appelle l'api distante pour récupérer les résultats de la recherche
            string jsonResult = MovieDbAPI_Helper.Call_MovieDbAPI(query, page, UrlSerie);
            var items = JsonConvert.DeserializeObject<Serie.RootObject>(jsonResult);
            var results = items.results;
            // on ajoute tous les résultats à notre bdd
            foreach (var item in results)
            {
                if (item.original_language == "fr" || item.original_language == "en")
                {
                    string UrlSerieDetails = "https://api.themoviedb.org/3/tv/" + item.id + "?api_key=a77cba9e9e7532eeccc7a2a0239bb7ff&language=fr-FR";
                    // on récupère les details des séries
                    string jsonDetails = MovieDbAPI_Helper.Call_Details_MovieDbAPI(UrlSerieDetails);
                    var itemsDetails = JsonConvert.DeserializeObject<Serie.Details.RootObject>(jsonDetails);

                    // on enregistre dans les variables les valeurs de chaque champs
                    var titre = item.original_name;
                    var synopsis = item.overview;
                    var date_sortie = itemsDetails.first_air_date;
                    if (date_sortie == null || date_sortie.Count() < 4)
                        date_sortie = "2000-01-01";
                    var url_affiche = itemsDetails.poster_path;
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
                    DataBase.AddSerie(titre, synopsis, date_sortie, url_affiche, url_bande_annonce, id_genre, id_moviedb);
                }
            }
        }


        /// <summary>
        /// Met à jour l'api locale à partir des résultats de l'api distante
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        public static void getResultUpdate(string query, int page)
        {
            var listeSerie = SerieDAO.upDateListSerie().Where(series => series.Titre.ToLower().Contains(query.ToLower()));

            // on crée une liste contenant les titres des séries présent dans notre api locale
            List<string> listeId_ApiLocale = new List<string>();

            foreach (var item in listeSerie)
            {
                listeId_ApiLocale.Add(item.Id_dbmovie.ToString());
            }

            // on crée une liste contenant les titre des séries présent dans l'api distante
            string UrlSerie = "https://api.themoviedb.org/3/search/tv?api_key=a77cba9e9e7532eeccc7a2a0239bb7ff&query=" + query + "&page=" + page + "&language=fr-FR";
            string jsonResult = MovieDbAPI_Helper.Call_MovieDbAPI(query, page, UrlSerie);
            var items = JsonConvert.DeserializeObject<Serie.RootObject>(jsonResult);
            var listeSerieDistante = items.results;

            foreach (var item in listeSerieDistante)
            {
                if (item.original_language == "fr" || item.original_language == "en")
                {
                    if (!listeId_ApiLocale.Contains(item.id.ToString()))
                    {
                        string UrlSerieDetails = "https://api.themoviedb.org/3/tv/" + item.id + "?api_key=a77cba9e9e7532eeccc7a2a0239bb7ff&language=fr-FR";
                        string jsonDetails = MovieDbAPI_Helper.Call_Details_MovieDbAPI(UrlSerieDetails);
                        var itemsDetails = JsonConvert.DeserializeObject<Serie.Details.RootObject>(jsonDetails);

                        var titre = item.original_name;
                        var synopsis = item.overview;
                        var date_sortie = itemsDetails.first_air_date;
                        if (date_sortie == null || date_sortie.Count() < 4)
                            date_sortie = "2000-01-01";
                        var url_affiche = itemsDetails.poster_path;
                        if (url_affiche == null)
                            url_affiche = "";
                        var url_bande_annonce = string.Empty;
                        string id_genre = string.Empty;
                        if (item.genre_ids.Count == 0)
                            id_genre = "-1";
                        else
                            id_genre = item.genre_ids[0].ToString();
                        var id_moviedb = item.id;

                        DataBase.AddSerie(titre, synopsis, date_sortie, url_affiche, url_bande_annonce, id_genre, id_moviedb);
                    }
                }
            }
        }

        /// <summary>
        /// retourne la liste des séries depuis l'api locale ou l'api distante
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IEnumerable<Serie> GetResults(string query, int page)
        {
            var listeSerie = SerieDAO.upDateListSerie().Where(series => series.Titre.ToLower().Contains(query.ToLower()));

            if (listeSerie.Count() == 0)
            {
                GetResultAll(query, page);
                listeSerie = SerieDAO.upDateListSerie().Where(series => series.Titre.ToLower().Contains(query.ToLower()));
                return listeSerie;
            }
                
            else
            {
                getResultUpdate(query, page);
                listeSerie = SerieDAO.upDateListSerie().Where(series => series.Titre.ToLower().Contains(query.ToLower()));
                return listeSerie;
            }
        }
    }
}
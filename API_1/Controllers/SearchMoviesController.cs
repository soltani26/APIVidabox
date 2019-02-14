using API_1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Http;

namespace API_1.Controllers
{
    public class Films
    {
        public string Titre { get; set; }
        public string synopsis { get; set; }
        public string dateSortie { get; set; }
        public int duree { get; set; }
        public string Url_Affiche { get; set; }
        public string url_BandeAnnoce { get; set; }
        public int Id_genre { get; set; }

        public Films(string Titre, string synopsis, string dateSortie, int duree, string Url_Affiche, string url_BandeAnnoce, int Id_genre)
        {
            this.Titre = Titre;
            this.synopsis = synopsis;
            this.dateSortie = dateSortie;
            this.duree = duree;
            this.Url_Affiche = Url_Affiche;
            this.url_BandeAnnoce = url_BandeAnnoce;
            this.Id_genre = Id_genre;
        }
    }
    public class RootObject
    {
        public int totalFilms;
        public List<Films> films = new List<Films>();
    }
    public class TheMovieDBMovieSearch
    {
        public class Result
        {
            public int vote_count { get; set; }
            public int id { get; set; }
            public bool video { get; set; }
            public double vote_average { get; set; }
            public string title { get; set; }
            public double popularity { get; set; }
            public string poster_path { get; set; }
            public string original_language { get; set; }
            public string original_title { get; set; }
            public List<object> genre_ids { get; set; }
            public string backdrop_path { get; set; }
            public bool adult { get; set; }
            public string overview { get; set; }
            public string release_date { get; set; }
        }
        public class RootObject
        {
            public int page { get; set; }
            public int total_results { get; set; }
            public int total_pages { get; set; }
            public List<Result> results { get; set; }
        }
    }
    public class TheMovieDBMovieGetDetails
    {
        public class Genre
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        public class ProductionCompany
        {
            public int id { get; set; }
            public string logo_path { get; set; }
            public string name { get; set; }
            public string origin_country { get; set; }
        }
        public class ProductionCountry
        {
            public string iso_3166_1 { get; set; }
            public string name { get; set; }
        }
        public class SpokenLanguage
        {
            public string iso_639_1 { get; set; }
            public string name { get; set; }
        }
        public class RootObject
        {
            public bool adult { get; set; }
            public string backdrop_path { get; set; }
            public object belongs_to_collection { get; set; }
            public int budget { get; set; }
            public List<Genre> genres { get; set; }
            public string homepage { get; set; }
            public int id { get; set; }
            public string imdb_id { get; set; }
            public string original_language { get; set; }
            public string original_title { get; set; }
            public string overview { get; set; }
            public double popularity { get; set; }
            public string poster_path { get; set; }
            public List<ProductionCompany> production_companies { get; set; }
            public List<ProductionCountry> production_countries { get; set; }
            public string release_date { get; set; }
            public int revenue { get; set; }
            public object runtime { get; set; }
            public List<SpokenLanguage> spoken_languages { get; set; }
            public string status { get; set; }
            public string tagline { get; set; }
            public string title { get; set; }
            public bool video { get; set; }
            public double vote_average { get; set; }
            public int vote_count { get; set; }
        }
    }
    public class TheMovieDBMovieVideo
    {
        public class Result
        {
            public string id { get; set; }
            public string iso_639_1 { get; set; }
            public string iso_3166_1 { get; set; }
            public string key { get; set; }
            public string name { get; set; }
            public string site { get; set; }
            public int size { get; set; }
            public string type { get; set; }
        }

        public class RootObject
        {
            public int id { get; set; }
            public List<Result> results { get; set; }
        }
    }

    public class SearchMoviesController : ApiController
    {
        public static TheMovieDBMovieGetDetails.RootObject apiGetDetails(int filmId)
        {
            //FAIRE REQUETE API DISTANTE
            string url = "https://api.themoviedb.org/3/movie/" + filmId + "?api_key=a77cba9e9e7532eeccc7a2a0239bb7ff";
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            WebReq.Method = "GET";
            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
            string jsonString = string.Empty;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }
            //CONVERTIT LE JSON EN OBJ
            return JsonConvert.DeserializeObject<TheMovieDBMovieGetDetails.RootObject>(jsonString);
        }

        public static TheMovieDBMovieVideo.RootObject apiGetVideo(int filmId)
        {
            //FAIRE REQUETE API DISTANTE
            string jsonString = string.Empty;
            string url = "http://api.themoviedb.org/3/movie/" + filmId + "/videos?api_key=a77cba9e9e7532eeccc7a2a0239bb7ff";
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            WebReq.Method = "GET";
            try
            {
                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
                using (Stream stream = WebResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                    jsonString = reader.ReadToEnd();
                }
            }
            catch (Exception ex) { }
            //CONVERTIT LE JSON EN OBJ
            return JsonConvert.DeserializeObject<TheMovieDBMovieVideo.RootObject>(jsonString);
        }

        // GET: api/SearchMovies
        [Route("search/movie")]
        public RootObject Get(string query, string page)
        {
            //FAIRE REQUETE API DISTANTE
            string url = "https://api.themoviedb.org/3/search/movie?api_key=a77cba9e9e7532eeccc7a2a0239bb7ff&query=" + query + "&language=fr-FR&page=" + page + "&include_adult=false";
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            WebReq.Method = "GET";
            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
            string jsonString = string.Empty;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }
            //CONVERTIT LE JSON EN OBJ
            var items = JsonConvert.DeserializeObject<TheMovieDBMovieSearch.RootObject>(jsonString);

            //POUR CHAQUE FILM, AJOUTER EN BDD LOCAL SI IL N'EXISTE PAS
            for (int i = 0; i < items.results.Count; i++)
            {
                string Titre = items.results[i].title;
                string synopsis = items.results[i].overview;
                string dateSortie = items.results[i].release_date;
                string Url_Affiche = items.results[i].poster_path == null ? string.Empty : items.results[i].poster_path;

                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("Titre", Titre);
                var currentResult = DataBase.select("SELECT Titre FROM Films WHERE Titre = @Titre",
                                        parameters);
                if (currentResult.Rows.Count == 0)
                {
                    //FAIRE LA REQUETE API DISTANCE NUMERO 2
                    var itemDetails = apiGetDetails(items.results[i].id);

                    //FAIRE REQUETE API DISTANTE POUR RECUPERER L'URL BANDE ANNONCE
                    var itemVideo = apiGetVideo(items.results[i].id);

                    string duree = string.Empty;
                    if (itemDetails.runtime != null)
                        duree = Convert.ToInt32(itemDetails.runtime).ToString();

                    string url_BandeAnnoce = string.Empty;
                    if (itemVideo.results.Count > 0)
                    {
                        if (itemVideo.results[0].site == "YouTube")
                            url_BandeAnnoce = "https://www.youtube.com/watch?v=" + itemVideo.results[0].key;
                    }

                    string Id_Genre = "-1";
                    if (itemDetails.genres.Count > 0)
                        Id_Genre = itemDetails.genres[0].id.ToString();

                    //AJOUT DU FILM EN DBB
                    DataBase.AddMovie(Titre, synopsis, dateSortie, duree, Url_Affiche, url_BandeAnnoce, Id_Genre);
                }
            }

            //RECUP LES FILMS DANS NOTRE BDD ET RETOURNE LA LISTE DES FILMS
            Dictionary<string, string> resultsParameters = new Dictionary<string, string>();
            var results = DataBase.select("SELECT * FROM Films WHERE Titre LIKE '%" + query + "%'",
                                           resultsParameters);
            RootObject rootObject = new RootObject();
            for (int i = 0; i < results.Rows.Count; i++)
            {
                string Titre = results.Rows[i][1].ToString();
                string synopsis = results.Rows[i][2].ToString();
                string dateSortie = results.Rows[i][3].ToString();
                int duree = Convert.ToInt32(results.Rows[i][4]);
                string Url_Affiche = results.Rows[i][5].ToString();
                string url_BandeAnnoce = results.Rows[i][6].ToString();
                int Id_genre = Convert.ToInt32(results.Rows[i][7]);

                rootObject.films.Add(new Films(Titre, synopsis, dateSortie, duree, Url_Affiche, url_BandeAnnoce, Id_genre));
            }
            rootObject.totalFilms = rootObject.films.Count;

            return rootObject;
        }

        // GET: api/SearchMovies/5
        public string Get(int id)
        {
            return "SearchMoviesController";
        }

        // POST: api/SearchMovies
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SearchMovies/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SearchMovies/5
        public void Delete(int id)
        {
        }
    }
}

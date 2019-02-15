using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_1.Models
{
    public class Film
    {

        public int Id_Films { get; set; }
        public string Titre { get; set; }
        public string synopsis { get; set; }
        public string dateSortie { get; set; }
        public int duree { get; set; }
        public string Url_Affiche { get; set; }
        public string url_BandeAnnonce { get; set; }
        public int Id_genre { get; set; }
        public int Id_dbmovie { get; set; }

        public Film(int Id_Films, string Titre, string synopsis, string dateSortie, int duree, string Url_Affiche, string url_BandeAnnonce, int Id_genre, int Id_dbmovie)
        {
            this.Id_Films = Id_Films;
            this.Titre = Titre;
            this.synopsis = synopsis;
            this.dateSortie = dateSortie;
            this.duree = duree;
            this.Url_Affiche = Url_Affiche;
            this.url_BandeAnnonce = url_BandeAnnonce;
            this.Id_genre = Id_genre;
            this.Id_dbmovie = Id_dbmovie;
        }

        public class Search
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
                public List<int> genre_ids { get; set; }
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

        public class Details
        {
            ////////DETAILS/////////
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
                public int runtime { get; set; }
                public List<SpokenLanguage> spoken_languages { get; set; }
                public string status { get; set; }
                public string tagline { get; set; }
                public string title { get; set; }
                public bool video { get; set; }
                public double vote_average { get; set; }
                public int vote_count { get; set; }
            }
        }
    }
        



       

       
}
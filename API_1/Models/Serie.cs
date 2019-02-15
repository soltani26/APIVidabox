using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_1.Models
{


    public class Serie
    {

        public int Id_Serie { get; set; }
        public string Titre { get; set; }
        public string Synopsis { get; set; }
        public string Date_sortie { get; set; }
        public string Url_affiche { get; set; }
        public string Url_bande_annonce { get; set; }
        public int Id_genre { get; set; }
        public int Id_dbmovie { get; set; }


        public Serie(int Id_Serie, string Titre, string Synopsis, string Date_sortie, string Url_affiche, string Url_bande_annonce, int Id_genre, int Id_dbmovie )
        {
            this.Id_Serie = Id_Serie;
            this.Titre = Titre;
            this.Synopsis = Synopsis;
            this.Date_sortie = Date_sortie;
            this.Url_affiche = Url_affiche;
            this.Url_bande_annonce = Url_bande_annonce;
            this.Id_genre = Id_genre;
            this.Id_dbmovie = Id_dbmovie;
        }


        

        public class Result
        {
            public string original_name { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int vote_count { get; set; }
            public double vote_average { get; set; }
            public string poster_path { get; set; }
            public string first_air_date { get; set; }
            public double popularity { get; set; }
            public List<object> genre_ids { get; set; }
            public string original_language { get; set; }
            public string backdrop_path { get; set; }
            public string overview { get; set; }
            public List<object> origin_country { get; set; }
        }

        public class RootObject
        {
            public int page { get; set; }
            public int total_results { get; set; }
            public int total_pages { get; set; }
            public List<Result> results { get; set; }
        }

        public class Details
        {

            public class CreatedBy
            {
                public int id { get; set; }
                public string credit_id { get; set; }
                public string name { get; set; }
                public int gender { get; set; }
                public string profile_path { get; set; }
            }

            public class Genre
            {
                public int id { get; set; }
                public string name { get; set; }
            }

            public class LastEpisodeToAir
            {
                public string air_date { get; set; }
                public int episode_number { get; set; }
                public int id { get; set; }
                public string name { get; set; }
                public string overview { get; set; }
                public string production_code { get; set; }
                public int season_number { get; set; }
                public int show_id { get; set; }
                public string still_path { get; set; }
                public decimal vote_average { get; set; }
                public int vote_count { get; set; }
            }

            public class Network
            {
                public string name { get; set; }
                public int id { get; set; }
                public string logo_path { get; set; }
                public string origin_country { get; set; }
            }

            public class ProductionCompany
            {
                public int id { get; set; }
                public string logo_path { get; set; }
                public string name { get; set; }
                public string origin_country { get; set; }
            }

            public class Season
            {
                public string air_date { get; set; }
                public int episode_count { get; set; }
                public int id { get; set; }
                public string name { get; set; }
                public string overview { get; set; }
                public string poster_path { get; set; }
                public int season_number { get; set; }
            }

            public class RootObject
            {
                public string backdrop_path { get; set; }
                public List<CreatedBy> created_by { get; set; }
                public List<int> episode_run_time { get; set; }
                public string first_air_date { get; set; }
                public List<Genre> genres { get; set; }
                public string homepage { get; set; }
                public int id { get; set; }
                public bool in_production { get; set; }
                public List<string> languages { get; set; }
                public string last_air_date { get; set; }
                public LastEpisodeToAir last_episode_to_air { get; set; }
                public string name { get; set; }
                public object next_episode_to_air { get; set; }
                public List<Network> networks { get; set; }
                public int number_of_episodes { get; set; }
                public int number_of_seasons { get; set; }
                public List<string> origin_country { get; set; }
                public string original_language { get; set; }
                public string original_name { get; set; }
                public string overview { get; set; }
                public double popularity { get; set; }
                public string poster_path { get; set; }
                public List<ProductionCompany> production_companies { get; set; }
                public List<Season> seasons { get; set; }
                public string status { get; set; }
                public string type { get; set; }
                public double vote_average { get; set; }
                public int vote_count { get; set; }
            }

        }

    }
}
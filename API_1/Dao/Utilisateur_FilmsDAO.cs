using API_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_1.Dao
{
    /// <summary>
    /// Permet d'ajouter, de supprimer et d'obtenir la liste des films ajouté par l'utilisateur
    /// </summary>
    public class Utilisateur_FilmsDAO
    {
        /// <summary>
        /// Vérifie si le film est détenue par l'utilisateur
        /// </summary>
        /// <param name="Id_User"></param>
        /// <param name="Id_Films"></param>
        /// <returns></returns>
        public static bool checkIfAlreadyExist(string Id_User, string Id_Films)
        {
            bool alreadyExist = false;

            string query = "Select * from Utilisateur_Films WHERE Id_User=@Id_User AND Id_Films=@Id_Films";

            Dictionary<string, string> datas = new Dictionary<string, string>();
            datas.Add("@Id_User", Id_User);
            datas.Add("@Id_Films", Id_Films);

            var results = DataBase.select(query, datas);
            if (results.Count > 0)
                alreadyExist = true;

            return alreadyExist;
        }


        /// <summary>
        /// Permet d'obtenir la liste des films détenues par l'utilisateur
        /// </summary>
        /// <param name="Id_User"></param>
        /// <returns></returns>
        public static IEnumerable<Film> getUtilisateur_Films(string Id_User)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@Id_user", Id_User);

            List<Film> listeUtilisateurFilms = new List<Film>();
            var results = DataBase.select(@"SELECT Films.Id_Films, Films.Titre, Films.synopsis, Films.dateSortie, Films.duree,                                          Films.Url_Affiche, Films.url_BandeAnnoce, Films.Id_genre, Utilisateur_Films.Id_Utilisateur_Films
                                            FROM Films
                                            INNER JOIN Utilisateur_Films ON Films.Id_Films = Utilisateur_Films.Id_Films
                                            INNER JOIN Utilisateur ON Utilisateur_Films.Id_User = Utilisateur.Id_user
                                            WHERE Utilisateur.Id_user = @Id_user", parameters);

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

                listeUtilisateurFilms.Add(new Film(id, titre, synopsis, dateSortie, duree, urlAffiche, urlBandeAnnonce, idGenre, idDbMovie));
            }

            return listeUtilisateurFilms;
        }


        /// <summary>
        /// Permet d'ajouter le film sélectionné à la bibiothèque de l'utlisateur
        /// </summary>
        /// <param name="Id_User"></param>
        /// <param name="Id_Films"></param>
        public static void addUtilisateur_Films(string Id_User, string Id_Films)
        {
            bool alreadyExist = checkIfAlreadyExist(Id_User, Id_Films);

            if (!alreadyExist)
                DataBase.AddUtilisateur_Films(Id_User, Id_Films);
        }


        /// <summary>
        /// Permet de supprimer le film sélectionner de la bibliothèque de l'utilisateur
        /// </summary>
        /// <param name="Id_User"></param>
        /// <param name="Id_Films"></param>
        public static void removeUtilisateur_Films(string Id_User, string Id_Films)
        {
            DataBase.RemoveUtilisateur_Films(Id_User, Id_Films);
        }

        

    }
}
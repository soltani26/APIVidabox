using API_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_1.Dao
{
    /// <summary>
    /// Permet de vérifier si l'utilisateur existe en base de donnée
    /// </summary>
    public class UtilisateurDAO
    {
        /// <summary>
        /// Récupère un utilisateur en fonction de son email et son mot de passe
        /// </summary>
        /// <param name="email"></param>
        /// <param name="motPasse"></param>
        /// <returns></returns>
        public static Users getUser(string email, string motPasse)
        {
            string userQuery = "Select * from Utilisateur WHERE email=@email AND motPasse=@motPasse";
            Dictionary<string, string> datas = new Dictionary<string, string>();

            datas.Add("@email", email);
            datas.Add("@motPasse", motPasse);

            var results = DataBase.select(userQuery, datas);
            Users user = null;

            foreach (var item in results)
            {
                var id = Convert.ToInt32(item["Id_user"]);
                var nom = item["nom"];
                var prenom = item["prenom"];
                var dateNaissance = item["dateNaissance"];
                var _email = item["email"];
                var _motPasse = item["motPasse"];

                user = new Users(id, nom, prenom, dateNaissance, _email, _motPasse);

            }
            return user;

        }
    }
}
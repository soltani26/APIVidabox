using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_1.Models
{
   
    public class Users
    {
        public int Id_user { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string dateNaissance { get; set; }
        public string email { get; set; }
        public string motPasse { get; set; }

        /// <summary>
        /// Constructeur de la classe Users
        /// </summary>
        /// <param name="id_user"></param>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="dateNaissance"></param>
        /// <param name="email"></param>
        /// <param name="motPasse"></param>
        public Users(int id_user, string nom, string prenom, string dateNaissance, string email, string motPasse)
        {
            Id_user = id_user;
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.email = email;
            this.motPasse = motPasse;
        }
    }
}
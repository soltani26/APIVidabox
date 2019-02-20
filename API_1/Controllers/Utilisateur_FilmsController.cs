using API_1.Dao;
using API_1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace API_1.Controllers
{
    public class Utilisateur_FilmsController : ApiController
    {
        // GET: api/Utilisateur_Films/Get
        [HttpGet]
        [Route("api/Utilisateur_Films/Get")]
        public Film.RootObject Get_Utilisateur_Films(string Id_User)
        {
            Film.RootObject root = new Film.RootObject();
            foreach (var item in Utilisateur_FilmsDAO.getUtilisateur_Films(Id_User))
            {
                root.films.Add(item);
            }
            root.totalFilms = root.films.Count();
            return root;
        }

        // POST: api/Utilisateur_Films/Add
        [HttpPost]
        [Route("api/Utilisateur_Films/Add")]
        public void Post([FromBody]Dictionary<string, string> value)
        {
            Utilisateur_FilmsDAO.addUtilisateur_Films(value["Id_User"], value["Id_Films"]);
        }

        // DELETE: api/Utilisateur_Films/Delete
        [HttpDelete]
        [Route("api/Utilisateur_Films/Delete")]
        public void Delete(string Id_User, string Id_Films)
        {
            Utilisateur_FilmsDAO.removeUtilisateur_Films(Id_User, Id_Films);
        }
    }
}

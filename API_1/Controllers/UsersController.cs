using API_1.Dao;
using API_1.Models;
using System.Web.Http;

namespace API_1.Controllers
{
    public class UsersController : ApiController
    {
        // GET: api/Users?email=toto@gmail.com&motPasse=toto
        public Users Get(string email, string motPasse)
        {
            return UtilisateurDAO.getUser(email, motPasse);
        }
    }
}

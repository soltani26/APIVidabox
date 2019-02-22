using API_1.Dao;
using API_1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace API_1.Controllers
{
    /// <summary>
    /// Controleur des films
    /// </summary>
    public class FilmController : ApiController
    {
        // GET: api/Film
        public IEnumerable<Film> Get()
        {
            return FilmDAO.listFilms();
        }

        // GET: api/Film?query=harry&page=1
        public Film.RootObject Get(string query, int page)
        {
            Film.RootObject listSearchFilms = new Film.RootObject();
            foreach (var item in FilmDAO.GetResults(query, page))
            {
                listSearchFilms.films.Add(item);
            }
            listSearchFilms.totalFilms = listSearchFilms.films.Count();

            return listSearchFilms;
        }
    }
}

using API_1.Dao;
using API_1.Models;
using API_1.Utils_Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_1.Controllers
{
    public class FilmController : ApiController
    {
        // GET: api/Film
        public IEnumerable<Film> Get()
        {
            return FilmDAO.upDateListFilms();
        }

        // GET: api/Film/5
        public IEnumerable<Film> Get(string query, int page)
        {

            return FilmDAO.GetResults(query, page);
        }

        // POST: api/Film
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Film/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Film/5
        public void Delete(int id)
        {
        }
    }
}

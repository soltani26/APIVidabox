using API_1.Dao;
using API_1.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace API_1.Controllers
{
    public class SerieController : ApiController
    {


        //GET: api/Serie
        public IEnumerable<Serie> Get()
        {
            return SerieDAO.upDateListSerie();
        }


        //GET: api/Serie/flash
        public IEnumerable<Serie> Get(string query, int page)
        {
            return SerieDAO.GetResults(query, page); 
        }

        // POST: api/Serie
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Serie/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Serie/5
        public void Delete(int id)
        {
        }
    }
}

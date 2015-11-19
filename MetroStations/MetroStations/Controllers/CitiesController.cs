using MetroStations.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace MetroStations.Controllers
{
    /// <summary>
    /// Base controller for Cities queries
    /// </summary>
    public class CitiesController : ApiController
    {
        /// <summary>
        /// Get list of available cities
        /// </summary>
        /// <returns><c>IEnumerable</c> of City type</returns>
        public IEnumerable<City> Get()
        {
            // TODO: Move to database
            var cities = new List<City>()
            {
                new City() { ID = 1, Name = "Moscow" },
                new City() { ID = 2, Name = "Saint Petersburg" }
            };

            return cities;
        }
    }
}

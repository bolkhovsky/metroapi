using MetroApi.Core.Models;
using MetroApi.Core.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.OutputCache.V2.TimeAttributes;

namespace MetroApi.Web.Controllers
{
    /// <summary>
    /// Base controller for Cities queries
    /// </summary>
    public class CitiesController : ApiController
    {
        private readonly IMetroService _metroService;

        public CitiesController(IMetroService metroService)
        {
            if (metroService == null)
                throw new ArgumentNullException("metroService");
            _metroService = metroService;
        }

        /// <summary>
        /// Get list of available cities
        /// </summary>
        /// <returns><c>IEnumerable</c> of City type</returns>
        [CacheOutputUntilThisMonth(25)]
        public IEnumerable<City> Get()
        {
            return _metroService.GetCities();                        
        }
    }
}

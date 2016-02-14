using System.Web.Http;
using WebApi.OutputCache.V2.TimeAttributes;
using MetroApi.Core.Models;
using MetroApi.Core.Services;
using System;
using MetroApi.Core.Exceptions;

namespace MetroApi.Web.Controllers
{
    /// <summary>
    /// Base controller for Metro stations queries
    /// </summary>
    [RoutePrefix("api/metro")]
    public class MetroController : ApiController
    {
        private readonly IMetroService _metroService;

        public MetroController(IMetroService metroService)
        {

            if (metroService == null)
                throw new ArgumentNullException("metroService");
            _metroService = metroService;
        }

        /// <summary>
        /// Get list of metro stations in target city
        /// </summary>
        /// <param name="cityId">City ID, can be taken from api/cities</param>
        /// <returns><c>IEnumerable</c> of MetroStation type</returns>
        [CacheOutputUntilThisMonth(25)]
        [Route("{cityId}")]
        public City GetCityMetro(string cityId)
        {
            try
            {
                return _metroService.GetCitySchema(cityId);
            }
            catch(CityNotFound)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
        }
    }
}

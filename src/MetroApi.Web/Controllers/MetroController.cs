using System.Web.Http;
using WebApi.OutputCache.V2.TimeAttributes;
using MetroApi.Core.Models;
using MetroApi.Core.Services;
using System;
using MetroApi.Core.Exceptions;
using MetroApi.Core;

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

        [Route("test")]
        [CacheOutputUntilThisMonth(25)]
        public string GetTest()
        {
            return "Hello, World!";
        }

        /// <summary>
        /// Get list of metro stations in Saint Petersburg
        /// </summary>
        /// <returns><c>IEnumerable</c> of MetroStation type</returns>
        [Route("spb")]
        [CacheOutputUntilThisMonth(25)]
        public City GetSaintPetersburgMetro()
        {
            return GetCityMetro(Constants.CityIds.SaintPetersburg);
        }

        /// <summary>
        /// Get list of metro stations in Moscow
        /// </summary>
        /// <returns><c>IEnumerable</c> of MetroStation type</returns>
        [Route("moscow")]
        [CacheOutputUntilThisMonth(25)]
        public City GetMoscowMetro()
        {
            return GetCityMetro(Constants.CityIds.Moscow);
        }

        /// <summary>
        /// Get list of metro stations in target city
        /// </summary>
        /// <param name="cityId">City ID, can be taken from api/cities</param>
        /// <returns><c>IEnumerable</c> of MetroStation type</returns>
        [Route("{cityId}")]
        [CacheOutputUntilThisMonth(25)]
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

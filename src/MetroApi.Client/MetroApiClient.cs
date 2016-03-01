using MetroApi.Core;
using MetroApi.Core.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MetroApi.Client
{
    public class MetroApiClient
    {
        private readonly Uri _baseUri;
        private readonly HttpMessageHandler _handler;

        public MetroApiClient()
            : this(new Uri("http://metroapi.ru/"))
        {
        }

        public MetroApiClient(HttpMessageHandler handler)
            : this(new Uri("http://metroapi.ru/"))
        {
            if (handler == null)
                throw new ArgumentNullException("handler");
            _handler = handler;
        }

        public MetroApiClient(Uri baseUri)
        {
            if (baseUri == null)
                throw new ArgumentNullException("baseUrl");
            _baseUri = baseUri;
        }

        public Task<City> GetSaintPetersburgMetro()
        {
            return GetCityMetro(Constants.CityIds.SaintPetersburg);
        }

        public Task<City> GetMoscowMetro()
        {
            return GetCityMetro(Constants.CityIds.Moscow);
        }

        public async Task<City> GetCityMetro(string cityId)
        {
            using (var client = _handler == null 
                ? new HttpClient() 
                : new HttpClient(_handler))
            {
                client.BaseAddress = _baseUri;
                var response = await client.GetAsync(string.Format("api/metro/{0}", cityId));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<City>();
            }
        }
    }
}

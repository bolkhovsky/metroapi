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

        const string ApiUrl = "https://api.hh.ru";

        public MetroApiClient()
            : this(new Uri(ApiUrl))
        {
        }

        public MetroApiClient(HttpMessageHandler handler)
            : this(new Uri(ApiUrl))
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

        public async Task<City> GetCityMetro(int cityId)
        {
            using (var client = _handler == null 
                ? new HttpClient() 
                : new HttpClient(_handler))
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(string.Format("{0}/metro/{1}", ApiUrl, cityId)),
                    Method = HttpMethod.Get,
                };
                request.Headers.Add("User-Agent", "MetroApi.Client (ilya@721lab.com)");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<City>();
            }
        }
    }
}

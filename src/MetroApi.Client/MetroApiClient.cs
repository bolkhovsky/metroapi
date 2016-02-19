using MetroApi.Core;
using MetroApi.Core.Models;
using RestSharp;
using System;

namespace MetroApi.Client
{
    public class MetroApiClient
    {
        private readonly Uri _baseUri;

        public MetroApiClient()
            : this(new Uri("http://metroapi.ru/api/"))
        {
        }

        public MetroApiClient(Uri baseUri)
        {
            if (baseUri == null)
                throw new ArgumentNullException("baseUrl");
            _baseUri = baseUri;
        }

        public City GetSaintPetersburgMetro()
        {
            return GetCityMetro(Constants.CityIds.SaintPetersburg);
        }

        public City GetMoscowMetro()
        {
            return GetCityMetro(Constants.CityIds.Moscow);
        }

        public City GetCityMetro(string cityId)
        {
            var client = new RestClient();
            client.BaseUrl = _baseUri;

            var request = new RestRequest("api/metro/{cityId}");
            request.AddParameter("cityId", cityId, ParameterType.UrlSegment);

            var response = client.Execute<City>(request);
            
            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                throw new ApplicationException(message, response.ErrorException);
            }
            
            return response.Data;
        }
    }
}

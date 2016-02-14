using MetroApi.Core.Services;
using System.Collections.Generic;
using MetroApi.Core.Models;
using MetroApi.Core.Exceptions;
using System.Xml.Serialization;
using System.Xml;
using System;

namespace MetroApi.Xml.Services
{
    public class XmlMetroService : IMetroService
    {
        private readonly IDictionary<string, string> _citiesConfig;

        public XmlMetroService(IDictionary<string, string> citiesConfig)
        {
            if (citiesConfig == null)
                throw new ArgumentNullException("citiesConfig");
            _citiesConfig = citiesConfig;
        }

        public IEnumerable<City> GetCities()
        {
            var cities = new List<City>()
            {
                City.Moscow(),
                City.SaintPetersburg()
            };
            return cities;
        }

        public City GetCitySchema(string cityId)
        {
            if (!_citiesConfig.ContainsKey(cityId))
                throw new CityNotFound(cityId);

            var xmlSerializer = new XmlSerializer(typeof(City));
            using (var reader = XmlReader.Create(_citiesConfig[cityId]))
            {
                return (City)xmlSerializer.Deserialize(reader);
            }
        }
    }
}

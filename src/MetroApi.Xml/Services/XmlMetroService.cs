using MetroApi.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroApi.Core.Models;
using System;
using MetroApi.Core;
using MetroApi.Core.Exceptions;

namespace MetroApi.Xml.Services
{
    public class XmlMetroService : IMetroService
    {
        public XmlMetroService()
        {

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
            City city;
            switch (cityId)
            {
                case Constants.CityIds.Moscow:
                    city = City.Moscow();
                    //city.MetroLines = GetMoscowMetroSchema(contentElement);
                    break;

                case Constants.CityIds.SaintPetersburg:
                    city = City.SaintPetersburg();
                    //city.MetroLines = GetSpbMetroSchema(contentElement);
                    break;

                default:
                    throw new CityNotFound(cityId);
            }
            return city;
        }
    }
}

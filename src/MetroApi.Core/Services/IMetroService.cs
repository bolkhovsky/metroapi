using System.Collections.Generic;
using MetroApi.Core.Models;

namespace MetroApi.Core.Services
{
    public interface IMetroService
    {
        City GetCitySchema(string cityId);
        IEnumerable<City> GetCities();
    }
}

using Oceandata.WebApi;
using Oceandata.WebApi.Models;
using System.Collections.Generic;

namespace MetroStations.Models
{
    /// <summary>
    /// Represent information about city
    /// </summary>
    public class City
    {
        /// <summary>
        /// City ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// City name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of metro lines
        /// </summary>
        public List<MetroLine> MetroLines { get; set; }

        public static City N()
        {
            return new City
            {
                Id = "N",
                Name = "Default City"
            };
        }

        public static City Moscow()
        {
            return new City
            {
                Id = Constants.CityIds.Moscow,
                Name = "Moscow"
            };
        }

        public static City SaintPetersburg()
        {
            return new City
            {
                Id = Constants.CityIds.SaintPetersburg,
                Name = "Saint Petersburg"
            };
        }
    }
}
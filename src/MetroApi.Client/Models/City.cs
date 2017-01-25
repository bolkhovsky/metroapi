using System.Collections.Generic;

namespace MetroApi.Core.Models
{
    /// <summary>
    /// Represent information about city
    /// </summary>
    public class City
    {
        /// <summary>
        /// City ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// City name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of metro lines
        /// </summary>
        public List<MetroLine> Lines { get; set; }

        public static City Moscow()
        {
            return new City
            {
                Id = Constants.CityIds.Moscow,
                Name = "Москва"
            };
        }

        public static City SaintPetersburg()
        {
            return new City
            {
                Id = Constants.CityIds.SaintPetersburg,
                Name = "Санкт-Петербург"
            };
        }
    }
}
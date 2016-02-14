using System.Collections.Generic;
using System.Xml.Serialization;

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
        [XmlAttribute]
        public string Id { get; set; }

        /// <summary>
        /// City name
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// List of metro lines
        /// </summary>
        public List<MetroLine> MetroLines { get; set; }

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
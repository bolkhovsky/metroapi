using System.Xml.Serialization;

namespace MetroApi.Core.Models
{
    /// <summary>
    /// Represents metro station
    /// </summary>
    public class MetroStation
    {
        /// <summary>
        /// Station name
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        public MetroStation()
        {

        }

        public MetroStation(string name)
        {
            this.Name = name;
        }
    }
}
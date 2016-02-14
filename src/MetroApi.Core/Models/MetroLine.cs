using System.Collections.Generic;
using System.Xml.Serialization;

namespace MetroApi.Core.Models
{
    public class MetroLine
    {
        [XmlAttribute]
        public string Name { get; set; }

        public List<MetroStation> Stations { get; set; }

        public MetroLine()
        {

        }

        public MetroLine(string name)
        {
            this.Name = name;
            this.Stations = new List<MetroStation>();
        }
    }
}
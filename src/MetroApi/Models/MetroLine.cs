using MetroApi.Web.Models;
using System.Collections.Generic;

namespace MetroApi.Web.Models
{
    public class MetroLine
    {
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
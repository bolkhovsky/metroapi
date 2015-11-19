using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroStations.Models
{
    /// <summary>
    /// Represents metro station
    /// </summary>
    public class MetroStation
    {
        /// <summary>
        /// Line name
        /// </summary>
        public string LineName { get; set; }

        /// <summary>
        /// Station name
        /// </summary>
        public string Name { get; set; }
    }
}
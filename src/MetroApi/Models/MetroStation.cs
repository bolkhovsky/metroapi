namespace MetroApi.Web.Models
{
    /// <summary>
    /// Represents metro station
    /// </summary>
    public class MetroStation
    {
        /// <summary>
        /// Station name
        /// </summary>
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
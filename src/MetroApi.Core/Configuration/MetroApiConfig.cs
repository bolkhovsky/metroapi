using System.Configuration;

namespace MetroApi.Core.Configuration
{
    public class MetroApiConfig : ConfigurationSection
    {
        public static MetroApiConfig GetConfig()
        {
            return (MetroApiConfig)ConfigurationManager.GetSection("metroapi") ?? new MetroApiConfig();
        }

        [ConfigurationProperty("cities")]
        [ConfigurationCollection(typeof(Cities), AddItemName = "city")]
        public Cities Cities
        {
            get
            {
                object o = this["cities"];
                return o as Cities;
            }
        }

    }
}

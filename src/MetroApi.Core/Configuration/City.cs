using System.Configuration;

namespace MetroApi.Core.Configuration
{
    public class City : ConfigurationElement
    {
        [ConfigurationProperty("id", IsRequired = true)]
        public string Id
        {
            get
            {
                return this["id"] as string;
            }
        }

        [ConfigurationProperty("filepath", IsRequired = true)]
        public string Filepath
        {
            get
            {
                return this["filepath"] as string;
            }
        }
    }
}

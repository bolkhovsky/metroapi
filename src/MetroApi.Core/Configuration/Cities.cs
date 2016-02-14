using System.Configuration;

namespace MetroApi.Core.Configuration
{
    public class Cities : ConfigurationElementCollection
    {
        public City this[int index]
        {
            get
            {
                return base.BaseGet(index) as City;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public new City this[string responseString]
        {
            get { return (City)BaseGet(responseString); }
            set
            {
                if (BaseGet(responseString) != null)
                {
                    BaseRemoveAt(BaseIndexOf(BaseGet(responseString)));
                }
                BaseAdd(value);
            }
        }

        protected override System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new City();
        }

        protected override object GetElementKey(System.Configuration.ConfigurationElement element)
        {
            return ((City)element).Id;
        }
    }
}

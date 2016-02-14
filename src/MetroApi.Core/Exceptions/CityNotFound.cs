using System;

namespace MetroApi.Core.Exceptions
{
    public class CityNotFound : Exception
    {
        public CityNotFound(string cityId)
            : base(string.Format("{0} is not in the list of supported cities"))
        {

        }
    }
}

using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;

namespace MetroApi.Tests
{
    class TestWebApiResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            return new List<Assembly> { typeof(MetroApi.Web.WebApiApplication).Assembly };
        }
    }
}

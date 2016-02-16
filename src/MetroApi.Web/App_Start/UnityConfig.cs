using MetroApi.Core.Services;
using MetroApi.Xml.Services;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using Unity.WebApi;
using System.Web.Hosting;

namespace MetroApi.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            var configuration = Core.Configuration.MetroApiConfig.GetConfig();
            var citiesConfig = configuration.Cities
                .OfType<Core.Configuration.City>()
                .ToDictionary(x => x.Id, x => HostingEnvironment.MapPath(x.Filepath));

            var citiesDictionary = new InjectionConstructor(
                new InjectionParameter<IDictionary<string, string>>(citiesConfig));

            container.RegisterType<IMetroService, XmlMetroService>(citiesDictionary);
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
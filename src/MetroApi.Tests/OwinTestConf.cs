using MetroApi.Web;
using Owin;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace MetroApi.Tests
{
    class OwinTestConf
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Services.Replace(typeof(IAssembliesResolver), new TestWebApiResolver());
            config.MapHttpAttributeRoutes();
            UnityConfig.RegisterComponents(config);
            app.UseWebApi(config);
        }
    }
}

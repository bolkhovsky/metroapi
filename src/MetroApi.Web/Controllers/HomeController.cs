using System.Web.Mvc;

namespace MetroApi.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Metro Stations API Home Page";

            return View();
        }
    }
}

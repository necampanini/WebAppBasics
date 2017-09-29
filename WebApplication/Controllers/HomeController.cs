using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}
using System.Web.Mvc;

namespace RestApi.Controllers.Mvc
{
    public class CatalogueController : Controller
    {
        // GET: Catalogue
        public ActionResult Index()
        {
            return View();
        }
    }
}
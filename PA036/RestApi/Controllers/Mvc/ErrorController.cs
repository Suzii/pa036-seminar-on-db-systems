using System.Web.Mvc;

namespace RestApi.Controllers.Mvc
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult _404()
        {
            return View();
        }
    }
}
using System.Web.Mvc;

namespace RestApi.Controllers.Mvc
{
    public class TestScenariosController : Controller
    {
        // GET: TestScenarios
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Scenario1()
        {
            return View("Scenario1");
        }
    }
}
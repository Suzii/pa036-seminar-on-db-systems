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
        public ActionResult Scenario2()
        {
            return View("Scenario2");
        }
        public ActionResult Scenario3()
        {
            return View("Scenario3");
        }
    }
}
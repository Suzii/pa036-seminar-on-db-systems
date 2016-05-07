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
        public ActionResult Scenario4()
        {
            return View("Scenario4");
        }
        public ActionResult Scenario5()
        {
            return View("Scenario5");
        }
        public ActionResult Scenario6()
        {
            return View("Scenario6");
        }
        public ActionResult Scenario7()
        {
            return View("Scenario7");
        }
    }
}
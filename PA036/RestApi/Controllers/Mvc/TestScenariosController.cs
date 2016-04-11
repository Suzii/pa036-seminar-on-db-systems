using System.Threading.Tasks;
using System.Web.Mvc;

namespace RestApi.Controllers.Mvc
{
    public class TestScenariosController : Controller
    {
        // GET: TestScenarios
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> Scenario1()
        {
            return View("Scenario1");
        }
    }
}
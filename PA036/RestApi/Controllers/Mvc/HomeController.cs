using System.Threading.Tasks;
using System.Web.Mvc;

namespace RestApi.Controllers.Mvc
{
    public class HomeController : Controller
    {
        // GET: Home
        public async Task<ActionResult> Index()
        {
            return View();
        }
    }
}
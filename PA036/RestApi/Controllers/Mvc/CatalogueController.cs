using System.Threading.Tasks;
using System.Web.Mvc;

namespace RestApi.Controllers.Mvc
{
    public class CatalogueController : Controller
    {
        // GET: Catalogue
        public async Task<ActionResult> Index()
        {
            return View();
        }
    }
}
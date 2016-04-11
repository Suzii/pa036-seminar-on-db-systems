using System.Threading.Tasks;
using System.Web.Mvc;

namespace RestApi.Controllers.Mvc
{
    public class ErrorController : Controller
    {
        // GET: Error
        public async Task<ActionResult> _404()
        {
            return View();
        }
    }
}
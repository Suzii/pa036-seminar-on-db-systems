using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestApi.Controllers
{
    public class TestScenariosController : Controller
    {
        // GET: TestScenarios
        public ActionResult Index()
        {
            return View();
        }
    }
}
﻿using System.Web.Mvc;

namespace RestApi.Controllers.Mvc
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
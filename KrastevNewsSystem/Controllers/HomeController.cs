using KrastevNewsSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KrastevNewsSystem.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IKrastevNewsSystemPersister dataManager)
            : base(dataManager)
        { }
        public ActionResult Index()
        {

            return RedirectToAction("Index", "Article");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
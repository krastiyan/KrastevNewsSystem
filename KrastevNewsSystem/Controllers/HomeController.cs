using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KrastevNewsSystem.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            
            return View(this.PersistenceContext.Articles.Select(a =>
                 new KrastevNewsSystem.Models.NewsArticleViewModel() {
                     ArticleID = a.Id,
                     Title = a.Title,
                     ArticleAuthor = a.ArticleAuthor.UserName,
                     PostedOn = a.PostedOn,
                     Content = a.Content,
                     Comments = a.Comments
                 }
            ).ToList()
            );
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
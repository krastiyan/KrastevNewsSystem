using AutoMapper;
using KrastevNewsSystem.Data;
using KrastevNewsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KrastevNewsSystem.Controllers
{
    public class ArticleController : BaseController
    {

        public ArticleController(IKrastevNewsSystemPersister dataManager)
            :base(dataManager)
        {}
        public ActionResult Index()
        {
            ICollection<NewsArticle> dbArticles = this.DataManager.Articles.All().ToList();
            IEnumerable<NewsArticleViewModel> articles = dbArticles
                .Select(a => Mapper.Map<NewsArticle, NewsArticleViewModel>(a)).OrderByDescending(a => a.PostedOn)
            .ToList();
            return View(articles);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(NewsArticleViewModel theArticle)
        {
            var user = this.DataManager.Users.All().FirstOrDefault(u => u.UserName == HttpContext.User.Identity.Name);
            NewsArticle article = new Models.NewsArticle
            {
                Title = theArticle.Title,
                Content = theArticle.Content,
                ArticleAuthor = user,
                PostedOn = DateTime.Now
            };

            this.DataManager.Articles.Add(article);
            this.DataManager.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
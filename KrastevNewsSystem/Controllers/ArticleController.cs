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
        public ActionResult Index()
        {
            //var post = this.PersistenceContext.Posts.FirstOrDefault();

            ICollection<NewsArticleViewModel> articles = this.PersistenceContext.Articles.Select(a =>
            new NewsArticleViewModel()
            {
                ArticleID = a.Id,
                Title = a.Title,
                Content = a.Content,
                PostedOn = a.PostedOn,
                ArticleAuthor = a.ArticleAuthor.UserName,
                Comments = a.Comments
            }
            )
            .ToList();
            return View(articles);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NewsArticleViewModel theArticle)
        {
            var user = this.PersistenceContext.Users.FirstOrDefault(u => u.UserName == HttpContext.User.Identity.Name);
            NewsArticle article = new Models.NewsArticle
            {
                Title = theArticle.Title,
                Content = theArticle.Content,
                ArticleAuthor = user,
                PostedOn = DateTime.Now
            };

            this.PersistenceContext.Articles.Add(article);
            this.PersistenceContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
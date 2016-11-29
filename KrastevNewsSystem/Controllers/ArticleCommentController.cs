using KrastevNewsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KrastevNewsSystem.Controllers
{
    public class ArticleCommentController : BaseController
    {

        [HttpGet]
        public ActionResult Create(int commentedPostID)
        {
            return View(new NewsArticleCommentViewModel()
            {
                CommentedNewsArticleID = commentedPostID,
                CommentAuthor = HttpContext.User.Identity.Name
            });
        }

        [HttpPost]
        public ActionResult Create(NewsArticleCommentViewModel theComment)
        {
            var user = this.PersistenceContext.Users.FirstOrDefault(u => u.UserName == theComment.CommentAuthor);
            var article = this.PersistenceContext.Articles.FirstOrDefault(p => p.Id == theComment.CommentedNewsArticleID);
            NewsArticleComment comment = new Models.NewsArticleComment
            {
                Content = theComment.Content,
                CommentedArticle = article,
                CommentAuthor = user,
            };

            this.PersistenceContext.ArticlesComments.Add(comment);
            this.PersistenceContext.SaveChanges();

            return RedirectToAction("Index", "Post");
        }
    }
}
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
            var commentReplied = this.PersistenceContext.ArticlesComments.FirstOrDefault(c => c.Id == theComment.CommentRepliedToID);
            NewsArticleComment comment = new Models.NewsArticleComment
            {
                Content = theComment.Content,
                CommentedNewsArticle = article,
                CommentRepliedTo = commentReplied,
                CommentAuthor = user,
                PostedOn = DateTime.Now
            };

            this.PersistenceContext.ArticlesComments.Add(comment);
            this.PersistenceContext.SaveChanges();

            return RedirectToAction("Index", "Article");
        }

        [HttpGet]
        public ActionResult Reply(int commentedPostID, int commentID)
        {
            var repliedToComment = this.PersistenceContext.ArticlesComments.FirstOrDefault(c => c.Id == commentID);

            return View(new NewsArticleCommentViewModel()
            {
                CommentedNewsArticleID = commentedPostID,
                CommentRepliedToID = commentID,
                CommentRepliedToAuthor = repliedToComment.CommentAuthor.UserName,
                CommentRepliedToDate = repliedToComment.PostedOn,
                CommentAuthor = HttpContext.User.Identity.Name
            });
        }

        [HttpPost]
        public ActionResult Reply(NewsArticleCommentViewModel theComment)
        {
            var user = this.PersistenceContext.Users.FirstOrDefault(u => u.UserName == theComment.CommentAuthor);
            var article = this.PersistenceContext.Articles.FirstOrDefault(p => p.Id == theComment.CommentedNewsArticleID);
            var repliedToComment = this.PersistenceContext.ArticlesComments.FirstOrDefault(c => c.Id == theComment.CommentRepliedToID);

            NewsArticleComment comment = new Models.NewsArticleComment
            {
                CommentAuthor = user,
                PostedOn = DateTime.Now,
                Content = theComment.Content,
                CommentedNewsArticle = article,
                CommentRepliedTo = repliedToComment,
            };

            this.PersistenceContext.ArticlesComments.Add(comment);
            this.PersistenceContext.SaveChanges();

            return RedirectToAction("Index", "Article");
        }

        [HttpPost]
        public ActionResult Delete(int commentID)
        {
            var comment = this.PersistenceContext.ArticlesComments.FirstOrDefault(c => c.Id == commentID);

            this.PersistenceContext.ArticlesComments.Remove(comment);
            this.PersistenceContext.SaveChanges();

            return RedirectToAction("Index", "Article");
        }

    }
}
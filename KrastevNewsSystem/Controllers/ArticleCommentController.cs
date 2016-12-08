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
    public class ArticleCommentController : BaseController
    {
        public ArticleCommentController(IKrastevNewsSystemPersister dataManager)
            : base(dataManager)
        { }

        [HttpGet]
        [Authorize]
        public ActionResult Create(int commentedPostID)
        {
            var currentUserName = HttpContext.User.Identity.Name;
            if (currentUserName == null || currentUserName.Length < 1)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View(new NewsArticleCommentViewModel()
                {
                    CommentedNewsArticleID = commentedPostID,
                    CommentAuthor = currentUserName
                });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(NewsArticleCommentViewModel theComment)
        {
            var user = this.DataManager.Users.All().FirstOrDefault(u => u.UserName == theComment.CommentAuthor);
            var article = this.DataManager.Articles.All().FirstOrDefault(p => p.Id == theComment.CommentedNewsArticleID);
            var commentReplied = this.DataManager.ArticlesComments.All().FirstOrDefault(c => c.Id == theComment.CommentRepliedToID);
            NewsArticleComment comment = Mapper.Map<NewsArticleComment>(theComment);
            comment.CommentAuthor = user;
            comment.CommentedNewsArticle = article;
            comment.CommentRepliedTo = commentReplied;

            this.DataManager.ArticlesComments.Add(comment);
            this.DataManager.SaveChanges();

            return RedirectToAction("Index", "Article");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Reply(NewsArticleCommentViewModel theComment)
        {
            var user = this.DataManager.Users.All().FirstOrDefault(u => u.UserName == theComment.CommentAuthor);
            var article = this.DataManager.Articles.All().FirstOrDefault(p => p.Id == theComment.CommentedNewsArticleID);
            var repliedToComment = this.DataManager.ArticlesComments.All().FirstOrDefault(c => c.Id == theComment.CommentRepliedToID);

            NewsArticleComment comment = Mapper.Map<NewsArticleComment>(theComment);
            comment.CommentAuthor = user;
            comment.CommentedNewsArticle = article;
            comment.CommentRepliedTo = repliedToComment;
            //comment.PostedOn = DateTime.Now;
            //    new Models.NewsArticleComment
            //{
            //    CommentAuthor = user,
            //    PostedOn = DateTime.Now,
            //    Content = theComment.Content,
            //    CommentedNewsArticle = article,
            //    CommentRepliedTo = repliedToComment,
            //};

            this.DataManager.ArticlesComments.Add(comment);
            this.DataManager.SaveChanges();

            return RedirectToAction("Index", "Article");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Reply(int commentedPostID, int commentID)
        {
            var currentUserName = HttpContext.User.Identity.Name;
            if (currentUserName == null || currentUserName.Length < 1)
            {
                return RedirectToAction("Login", "Account");
            }

            var repliedToComment = this.DataManager.ArticlesComments.All().FirstOrDefault(c => c.Id == commentID);

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
        [Authorize]
        public ActionResult Delete(int commentID)
        {
            var comment = this.DataManager.ArticlesComments.All().FirstOrDefault(c => c.Id == commentID);

            this.DataManager.ArticlesComments.Delete(comment);
            this.DataManager.SaveChanges();

            return RedirectToAction("Index", "Article");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KrastevNewsSystem.Data;
using KrastevNewsSystem.Models;
using KrastevNewsSystem.Controllers;
using AutoMapper;

namespace KrastevNewsSystem.Areas.UsersFeatures.Controllers
{
    public class NewsArticleCommentsController : BaseController
    {
        public NewsArticleCommentsController(IKrastevNewsSystemPersister dataManager) : base(dataManager)
        { }

        // GET: UsersFeatures/NewsArticleCommentsController
        public ActionResult Index()
        {
            return View(base.DataManager.ArticlesComments.All().ToList());
        }

        // GET: UsersFeatures/NewsArticleCommentsController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsArticleComment newsArticleComment = base.DataManager.ArticlesComments.Find(id);
            if (newsArticleComment == null)
            {
                return HttpNotFound();
            }
            return View(newsArticleComment);
        }

        [HttpGet]
        [Authorize]
        // GET: UsersFeatures/NewsArticleCommentsController/Create
        public ActionResult Create(int commentedPostID)
        {
            var currentUserName = HttpContext.User.Identity.Name;
            if (currentUserName == null || currentUserName.Length < 1)
            {
                return RedirectToAction("Login", "./Account");
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

        // POST: UsersFeatures/NewsArticleCommentsController/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewsArticleCommentViewModel theComment)//[Bind(Include = "Id,Content,PostedOn")] 
        {
            if (ModelState.IsValid)
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

                return RedirectToAction("Index", "./../Home");
            }

            return View(theComment);
        }

        // GET: UsersFeatures/NewsArticleCommentsController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsArticleComment newsArticleComment = base.DataManager.ArticlesComments.Find(id);
            if (newsArticleComment == null)
            {
                return HttpNotFound();
            }
            return View(newsArticleComment);
        }

        // POST: UsersFeatures/NewsArticleCommentsController/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,PostedOn")] NewsArticleComment newsArticleComment)
        {
            if (ModelState.IsValid)
            {
                base.DataManager.ArticlesComments.Update(newsArticleComment);
                base.DataManager.ArticlesComments.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsArticleComment);
        }

        // GET: UsersFeatures/NewsArticleCommentsController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsArticleComment newsArticleComment = base.DataManager.ArticlesComments.Find(id);
            if (newsArticleComment == null)
            {
                return HttpNotFound();
            }
            return View(newsArticleComment);
        }

        // POST: UsersFeatures/NewsArticleCommentsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsArticleComment newsArticleComment = base.DataManager.ArticlesComments.Find(id);
            base.DataManager.ArticlesComments.Delete(newsArticleComment);
            base.DataManager.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        base.DataManager.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}

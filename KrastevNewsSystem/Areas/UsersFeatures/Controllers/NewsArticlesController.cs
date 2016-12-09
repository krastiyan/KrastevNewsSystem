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

namespace KrastevNewsSystem.Areas.UsersFeatures.Controllers
{
    public class NewsArticlesController : BaseController
    {
        public NewsArticlesController(IKrastevNewsSystemPersister dataManager) : base(dataManager)
        { }

        [Authorize]
        // GET: UsersFeatures/NewsArticles
        public ActionResult Index()
        {
            return View(base.DataManager.Articles.All().ToList());
        }

        [Authorize]
        // GET: UsersFeatures/NewsArticles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsArticle newsArticle = base.DataManager.Articles.Find(id);
            if (newsArticle == null)
            {
                return HttpNotFound();
            }
            return View(newsArticle);
        }

        [Authorize]
        // GET: UsersFeatures/NewsArticles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersFeatures/NewsArticles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,PostedOn")] NewsArticle newsArticle)
        {
            if (!ModelState.IsValid)
            {
                return View(newsArticle);
            }

            var user = this.DataManager.Users.All().FirstOrDefault(u => u.UserName == HttpContext.User.Identity.Name);
            NewsArticle article = new NewsArticle
            {
                Title = newsArticle.Title,
                Content = newsArticle.Content,
                ArticleAuthor = user,
                PostedOn = DateTime.Now
            };

            base.DataManager.Articles.Add(article);
            base.DataManager.Articles.SaveChanges();

            return RedirectToAction("Index", "./../Article");
        }

        [Authorize]
        // GET: UsersFeatures/NewsArticles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsArticle newsArticle = base.DataManager.Articles.Find(id);
            if (newsArticle == null)
            {
                return HttpNotFound();
            }
            return View(newsArticle);
        }

        // POST: UsersFeatures/NewsArticles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,PostedOn")] NewsArticle newsArticle)
        {
            if (ModelState.IsValid)
            {
                base.DataManager.Articles.Update(newsArticle);
                base.DataManager.Articles.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsArticle);
        }

        [Authorize]
        // GET: UsersFeatures/NewsArticles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsArticle newsArticle = base.DataManager.Articles.Find(id);
            if (newsArticle == null)
            {
                return HttpNotFound();
            }
            return View(newsArticle);
        }

        [Authorize]
        // POST: UsersFeatures/NewsArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsArticle newsArticle = base.DataManager.Articles.Find(id);
            base.DataManager.Articles.Delete(newsArticle);
            base.DataManager.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}

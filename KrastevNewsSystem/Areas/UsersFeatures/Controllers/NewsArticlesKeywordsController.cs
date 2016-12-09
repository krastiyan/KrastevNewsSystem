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
using AutoMapper;
using KrastevNewsSystem.Controllers;

namespace KrastevNewsSystem.Areas.UsersFeatures.Controllers
{
    public class NewsArticlesKeywordsController : BaseController
    {
        public NewsArticlesKeywordsController(IKrastevNewsSystemPersister dataManager) : base(dataManager)
        {
        }

        [Authorize]
        // GET: UsersFeatures/NewsArticlesKeywords
        public ActionResult Index()
        {
            //return View(base.DataManager.ArticleKeywords.ToList());
            ICollection<ArticleKeyword> dbKeywords = this.DataManager.ArticlesKeywords.All()
                .OrderBy(k => k.Keyword).ToList();
            IEnumerable<ArticleKeywordViewModel> keywords = dbKeywords
                .Select(k => Mapper.Map<ArticleKeyword, ArticleKeywordViewModel>(k))
            .ToList();
            return View(keywords);
        }

        [Authorize]
        // GET: UsersFeatures/NewsArticlesKeywords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleKeyword articleKeyword = base.DataManager.ArticlesKeywords.Find(id);
            if (articleKeyword == null)
            {
                return HttpNotFound();
            }
            return View(articleKeyword);
        }

        [Authorize]
        // GET: UsersFeatures/NewsArticlesKeywords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersFeatures/NewsArticlesKeywords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KeywordId,Keyword,IsStoryKeyword,ValidFrom,ValidTo")] ArticleKeyword articleKeyword)
        {
            if (ModelState.IsValid)
            {
                base.DataManager.ArticlesKeywords.Add(articleKeyword);
                base.DataManager.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(articleKeyword);
        }

        [Authorize]
        public ActionResult Invalidate(int keywordID)
        {
            ArticleKeyword keyword = this.DataManager.ArticlesKeywords.All().FirstOrDefault(kw => kw.KeywordId == keywordID);
            keyword.ValidTo = DateTime.Now;
            this.DataManager.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Enable(int keywordID)
        {
            ArticleKeyword keyword = this.DataManager.ArticlesKeywords.All().FirstOrDefault(kw => kw.KeywordId == keywordID);
            DateTime currentDate = DateTime.Now;
            keyword.ValidFrom = currentDate;
            if (keyword.ValidTo < currentDate)
            {
                keyword.ValidTo = null;
            }
            this.DataManager.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        // GET: UsersFeatures/NewsArticlesKeywords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleKeyword articleKeyword = base.DataManager.ArticlesKeywords.Find(id);
            if (articleKeyword == null)
            {
                return HttpNotFound();
            }
            return View(articleKeyword);
        }

        // POST: UsersFeatures/NewsArticlesKeywords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KeywordId,Keyword,IsStoryKeyword,ValidFrom,ValidTo")] ArticleKeyword articleKeyword)
        {
            if (ModelState.IsValid)
            {
                base.DataManager.ArticlesKeywords.Update(articleKeyword);
                base.DataManager.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(articleKeyword);
        }

        [Authorize]
        // GET: UsersFeatures/NewsArticlesKeywords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleKeyword articleKeyword = base.DataManager.ArticlesKeywords.Find(id);
            if (articleKeyword == null)
            {
                return HttpNotFound();
            }
            return View(articleKeyword);
        }

        [Authorize]
        // POST: UsersFeatures/NewsArticlesKeywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArticleKeyword articleKeyword = base.DataManager.ArticlesKeywords.Find(id);
            base.DataManager.ArticlesKeywords.Delete(articleKeyword);
            base.DataManager.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}

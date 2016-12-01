using AutoMapper;
using KrastevNewsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KrastevNewsSystem.Data;

namespace KrastevNewsSystem.Controllers
{
    public class ArticleKeywordsController : BaseController
    {
        public ArticleKeywordsController(IKrastevNewsSystemPersister dataManager) : base(dataManager)
        {
        }

        public ActionResult Index()
        {
            //var post = this.DataManager.Posts.FirstOrDefault();

            ICollection<ArticleKeyword> dbKeywords = this.DataManager.ArticlesKeywords.All()
                .OrderBy(k => k.Keyword).ToList();
            IEnumerable<ArticleKeywordViewModel> keywords = dbKeywords
                .Select(k => Mapper.Map<ArticleKeyword, ArticleKeywordViewModel>(k))
            .ToList();
            return View(keywords);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ArticleKeywordViewModel theKeyword)
        {
            ArticleKeyword keyword = Mapper.Map<ArticleKeyword>(theKeyword);
            //    new ArticleKeyword
            //{
            //    Keyword = theKeyword.Keyword,
            //    IsStoryKeyword = theKeyword.IsStoryKeyword,
            //    ValidFrom = theKeyword.ValidFrom,
            //    ValidTo = theKeyword.ValidTo
            //};

            this.DataManager.ArticlesKeywords.Add(keyword);
            this.DataManager.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Invalidate(int keywordID)
        {
            ArticleKeyword keyword = this.DataManager.ArticlesKeywords.All().FirstOrDefault(kw => kw.KeywordId == keywordID);
            keyword.ValidTo = DateTime.Now;//Will this update entity record in Database?
            this.DataManager.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Enable(int keywordID)
        {
            ArticleKeyword keyword = this.DataManager.ArticlesKeywords.All().FirstOrDefault(kw => kw.KeywordId == keywordID);
            DateTime currentDate = DateTime.Now;
            keyword.ValidFrom = currentDate;//Will this update entity record in Database?
            if (keyword.ValidTo < currentDate)
            {
                keyword.ValidTo = null;
            }
            this.DataManager.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
using KrastevNewsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KrastevNewsSystem.Controllers
{
    public class ArticleTaggingController : BaseController
    {
        /**
         * Should display ylist of all keywrods in DB no matter if valid
         **/
        public ActionResult Index(NewsArticleViewModel theArticle)
        {
            var article = this.PersistenceContext.Articles.FirstOrDefault(a => a.Id == theArticle.ArticleID);
            ICollection<ArticleKeywordViewModel> keywords = article.AssignedKeywords.Select(k =>
            new ArticleKeywordViewModel()
            {
                KeywordId = k.KeywordId,
                Keyword = k.Keyword,
                IsStoryKeyword = k.IsStoryKeyword,
                ValidFrom = k.ValidFrom,
                ValidTo = k.ValidTo,
                KeywordedArticles = k.KeywordedArticles
            }
            )
            .ToList();

            return View(keywords);
        }

        [HttpGet]
        public ActionResult AddKeywrodToArticle(NewsArticleViewModel theArticle)
        {
            var article = this.PersistenceContext.Articles.FirstOrDefault(a => a.Id == theArticle.ArticleID);
            DateTime currentDate = DateTime.Now;
            ICollection<ArticleKeyword> nonArticleKeywords = this.PersistenceContext.ArticlesKeywords.Where(k =>
            k.ValidFrom < currentDate && k.ValidTo > currentDate && !article.AssignedKeywords.Contains(k)
            )
            .ToList();

            return View(nonArticleKeywords.Select(k => new ArticleKeywordViewModel()
            {
                KeywordId = k.KeywordId,
                Keyword = k.Keyword,
                IsStoryKeyword = k.IsStoryKeyword,
                ValidFrom = k.ValidFrom,
                ValidTo = k.ValidTo
            }
            ));
        }

        [HttpPost]
        public ActionResult AddKeywrodToArticle(ArticleKeywordViewModel theKeyword, NewsArticleViewModel theArticle)
        {
            var article = this.PersistenceContext.Articles.FirstOrDefault(a => a.Id == theArticle.ArticleID);
            var keyword = this.PersistenceContext.ArticlesKeywords.FirstOrDefault(k => k.KeywordId == theKeyword.KeywordId);
            article.AssignedKeywords.Add(keyword);
            keyword.KeywordedArticles.Add(article);
            this.PersistenceContext.SaveChanges();
            return RedirectToAction("Index", "ArticleTagging");//ArticleTagging to be partial view controller?
        }
    }
}
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
    public class ArticleTaggingController : BaseController
    {
        public ArticleTaggingController(IKrastevNewsSystemPersister dataManager)
            : base(dataManager)
        { }
        /**
         * Should display ylist of all keywrods in DB no matter if valid
         **/
        [Authorize]
        public ActionResult Index(int theArticleID)
        {
            var article = this.DataManager.Articles.All().FirstOrDefault(a => a.Id == theArticleID);
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

            ViewBag.TheArticleID = theArticleID;

            return View(new ArticleTaggingViewModel()
            {
                taggedArticle = Mapper.Map<NewsArticleViewModel>(article),
                taggsList = keywords
            }
            );
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddKeywordToArticle(int theArticleID)
        {
            var article = this.DataManager.Articles.All().FirstOrDefault(a => a.Id == theArticleID);
            DateTime currentDate = DateTime.Now;
            ICollection<ArticleKeyword> nonArticleKeywords = null;
            if (article.AssignedKeywords.Count > 0)
            {
                ICollection<int> articleKeywordsIDs = new List<int>(article.AssignedKeywords.Count());
                foreach (var item in article.AssignedKeywords)
                {
                    articleKeywordsIDs.Add(item.KeywordId);
                }
                nonArticleKeywords = this.DataManager.ArticlesKeywords.All()
                    .Where(KeywordIsValid(currentDate))
                    .Where(k => !articleKeywordsIDs.Contains(k.KeywordId))
                .ToList();
                //&&
                //KeywordIsValid(currentDate)
                //k =>
                //k.ValidFrom < currentDate &&
                //    (k.ValidTo == null || k.ValidTo > currentDate)
                //    && 
                //!articleKeywordsIDs.Contains(k.KeywordId)
                //)
                //.ToList();
            }
            else
            {
                nonArticleKeywords = this.DataManager.ArticlesKeywords.All().Where(
                    KeywordIsValid(currentDate))
                .ToList();
                //    k =>
                //k.ValidFrom < currentDate &&
                //    (k.ValidTo == null || k.ValidTo > currentDate)
                //)
                //.ToList();
            }

            ViewBag.TheArticleID = theArticleID;
            ICollection<ArticleKeywordViewModel> result = nonArticleKeywords.Select(k => Mapper.Map<ArticleKeywordViewModel>(k)).ToList();
            return View(result);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddThisKeywordToArticle(int theKeywordID, int theArticleID)
        {
            var article = this.DataManager.Articles.All().FirstOrDefault(a => a.Id == theArticleID);
            var keyword = this.DataManager.ArticlesKeywords.All().FirstOrDefault(k => k.KeywordId == theKeywordID);
            article.AssignedKeywords.Add(keyword);
            keyword.KeywordedArticles.Add(article);
            this.DataManager.SaveChanges();
            return RedirectToAction("Index", "ArticleTagging", new { theArticleID = theArticleID });//ArticleTagging to be partial view controller?
        }


        [HttpGet]
        [Authorize]
        public ActionResult UntagThisKeywordFromArticle(int theKeywordID, int theArticleID)
        {
            var article = this.DataManager.Articles.All().FirstOrDefault(a => a.Id == theArticleID);
            var keyword = this.DataManager.ArticlesKeywords.All().FirstOrDefault(k => k.KeywordId == theKeywordID);
            article.AssignedKeywords.Remove(keyword);
            keyword.KeywordedArticles.Remove(article);
            this.DataManager.SaveChanges();
            return RedirectToAction("Index", "ArticleTagging", new { theArticleID = theArticleID });//ArticleTagging to be partial view controller?
        }
    }
}
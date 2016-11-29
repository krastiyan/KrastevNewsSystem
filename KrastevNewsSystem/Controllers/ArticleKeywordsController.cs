using KrastevNewsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KrastevNewsSystem.Controllers
{
    public class ArticleKeywordsController : BaseController
    {

        public ActionResult Index()
        {
            //var post = this.PersistenceContext.Posts.FirstOrDefault();

            ICollection<ArticleKeywordViewModel> keywords = this.PersistenceContext.ArticlesKeywords.Select(k =>
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
        public ActionResult Create(int commentedPostID)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ArticleKeywordViewModel theKeyword)
        {
            //var user = this.PersistenceContext.Users.FirstOrDefault(u => u.UserName == theComment.CommentAuthor);
            //var article = this.PersistenceContext.Articles.FirstOrDefault(p => p.Id == theComment.CommentedNewsArticleID);
            ArticleKeyword keyword = new Models.ArticleKeyword
            {
                Keyword = theKeyword.Keyword,
                IsStoryKeyword = theKeyword.IsStoryKeyword,
                ValidFrom = theKeyword.ValidFrom,
                ValidTo = theKeyword.ValidTo
            };

            this.PersistenceContext.ArticlesKeywords.Add(keyword);
            this.PersistenceContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
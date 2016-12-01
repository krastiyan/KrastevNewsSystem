using KrastevNewsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KrastevNewsSystem.Data;

namespace KrastevNewsSystem.Controllers
{
    public class ArticlesSearchController : BaseController
    {
        public ArticlesSearchController(IKrastevNewsSystemPersister dataManager) : base(dataManager)
        {
        }

        /**
* Index should display search page with free text search field and keywords dropdown listing all valid keywords
**/
        public ActionResult Index()
        {
            DateTime currentDate = DateTime.Now;
            ICollection<ArticleKeyword> validArticleKeywords = this.DataManager.ArticlesKeywords.All().Where(k =>
            k.ValidFrom < currentDate && k.ValidTo > currentDate
            )
            .ToList();

            return View(validArticleKeywords.Select(k => new ArticleKeywordViewModel()
            {
                KeywordId = k.KeywordId,
                Keyword = k.Keyword,
                IsStoryKeyword = k.IsStoryKeyword,
                ValidFrom = k.ValidFrom,
                ValidTo = k.ValidTo
            }
            ));
        }

        /**
         * Should Search result view be partial one OR to let user go to Search menu when new search is needed?
         **/
        [HttpPost]
        public ActionResult Search(ISet<string> freeTextSearchTerms, ISet<string> keywrodsSearchTerms)
        {
            List<NewsArticle> result = null, foundArticles = null;

            if (keywrodsSearchTerms != null && keywrodsSearchTerms.Count() > 0)
            {
                ICollection<ArticleKeyword> searchedKeywrods = this.DataManager.ArticlesKeywords.All().Where(k =>
                    keywrodsSearchTerms.Contains(k.Keyword)
                    /**
                     * No check of k validity made since in Where
                     * keywrodsSearchTerms is expected to contain only valid keywords: with ValidFrom > Datetime.Now < ValidTo
                    **/
                    )
                    .ToList();

                foundArticles = new List<NewsArticle>();
                foreach (ArticleKeyword keyword in searchedKeywrods)
                {
                    foundArticles.AddRange(keyword.KeywordedArticles);
                }

                result = foundArticles.Distinct().ToList();
            }

            if (freeTextSearchTerms != null && freeTextSearchTerms.Count() > 0)
            {
                char[] splitCriteria = new char[] { ' ', '\n', '\r', '\t' };
                if (result != null && result.Count() > 0)
                {
                    foundArticles = result.Where(a =>
                        freeTextSearchTerms.IsProperSubsetOf(a.Content.Split(splitCriteria, StringSplitOptions.RemoveEmptyEntries))
                        ).ToList();
                }
                else
                {
                    foundArticles = this.DataManager.Articles.All().Where(a =>
                        freeTextSearchTerms.IsProperSubsetOf(a.Content.Split(splitCriteria, StringSplitOptions.RemoveEmptyEntries))
                        ).ToList();
                }

                result = foundArticles.Distinct().ToList();
            }

            /**
             * View cshtml file should handle if controller doesn't return search result
             **/
            if (result == null || result.Count() < 1)
            {
                return View();
            }

            return View(result.Select(a => new NewsArticleViewModel()
            {
                ArticleID = a.Id,
                Title = a.Title,
                ArticleAuthor = a.ArticleAuthor.UserName,
                PostedOn = a.PostedOn
            }));
        }//HttpPost Search method end
    }
}
using KrastevNewsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KrastevNewsSystem.Data;
using System.Collections;
using System.Linq.Expressions;
using System.Web.Routing;
using AutoMapper;
using System.Text;

namespace KrastevNewsSystem.Controllers
{
    public class ArticlesSearchController : BaseController
    {
        static char[] splitCriteria = new char[] { ' ', '\n', '\r', '\t' };
        static string keywordsSeparator = "~";

        public ArticlesSearchController(IKrastevNewsSystemPersister dataManager) : base(dataManager)
        {
        }

        /**
* Index should display search page with free text search field and keywords dropdown listing all valid keywords
**/
        public ActionResult Index()
        {
            DateTime currentDate = DateTime.Now;
            var validArticleKeywords = this.DataManager.ArticlesKeywords.All()
                .Where(KeywordIsValid(currentDate));
                        //k =>
                        //k.ValidFrom < currentDate &&
                        //(k.ValidTo == null || k.ValidTo > currentDate)
                        //);

            var wordsToSelect = validArticleKeywords.Select(l => l.Keyword).ToList();

            return View(new SearchCriteriaViewModel()
            {
                freeTextSearchCriteria = "",
                keywordsValues = wordsToSelect
            }
            );
        }

        [HttpPost]
        public ActionResult SearchViaForm(SearchCriteriaViewModel searchCriteria)
        {
            StringBuilder builder = new StringBuilder();
            if (searchCriteria.keywordsValues != null && searchCriteria.keywordsValues.Count() > 0)
                foreach (var keyword in searchCriteria.keywordsValues)
                {
                    builder.Append(keyword + keywordsSeparator);
                }

            return RedirectToAction("Search",
                new
                {
                    freeTextSearchTermsList = searchCriteria.freeTextSearchCriteria,
                    keywrodsSearchTermsList = builder.ToString()
                });
        }
        public ActionResult SearchByKeyword(string keywordToSearch)
        {
            return RedirectToAction("Search",
                new
                {
                    freeTextSearchTermsList = "",
                    keywrodsSearchTermsList = keywordToSearch
                });
        }

        /**
         * Should Search result view be partial one OR to let user go to Search menu when new search is needed?
         **/

        public ActionResult Search(string freeTextSearchTermsList, string keywrodsSearchTermsList)
        {
            List<NewsArticle> result = null, foundArticles = null;

            if (keywrodsSearchTermsList != null && keywrodsSearchTermsList.Length > 0)
            {
                string[] keywrodsSearchTerms = keywrodsSearchTermsList.Split(new string[] { keywordsSeparator }, StringSplitOptions.RemoveEmptyEntries);

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

            if (freeTextSearchTermsList != null && freeTextSearchTermsList.Count() > 0)
            {
                string[] freeTextSearchTerms = freeTextSearchTermsList.Split(splitCriteria, StringSplitOptions.RemoveEmptyEntries);
                ISet<string> freeTextSearch = new HashSet<string>(freeTextSearchTerms);
                if (result != null && result.Count() > 0)
                {
                    foundArticles = result.AsQueryable().Where(TextHasSearchedWords(freeTextSearch)
                        ).ToList();
                }
                else
                {
                    foundArticles = this.DataManager.Articles.All().Where(TextHasSearchedWords(freeTextSearch)
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

            return View(result.Select(a => Mapper.Map<NewsArticleViewModel>(a)));
        }//HttpPost Search method end

        /**
         * Defines extension method for LINQ to be called in Where clause for NewsArticle entities
         **/
        public Expression<Func<NewsArticle, bool>> TextHasSearchedWords(ISet<string> searchTerms)
        {
            return a => a.Content.Length > 0 && searchTerms.All(t => a.Content.Contains(t));
        }
    }
}
using KrastevNewsSystem.Models;
using KrastevNewsSystem.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KrastevNewsSystem.Data;

namespace KrastevNewsSystem.Services
{
    public class NewsArticlesService : BaseService<NewsArticle>, IArticlesService
    {
        public NewsArticlesService(IKrastevNewsSystemPersister data) : base(data)
        {}

        public void Add(NewsArticle entity, string userName)
        {
            var user = new NewsUsersService(base.Data).GetAll().FirstOrDefault(u => u.UserName == userName);

            base.Add(entity);
        }

        public IOrderedEnumerable<NewsArticle> GetAllTaggedWithKeyWords(ISet<string> keywrodsSearchTerms)
        {
            List<NewsArticle> foundArticles = null;
            ICollection<ArticleKeyword> searchedKeywrods = new NewsArticlesKeywrodsService(base.Data)
                                                                .GetAll().ToList();

            foundArticles = new List<NewsArticle>();
            foreach (ArticleKeyword keyword in searchedKeywrods)
            {
                foundArticles.AddRange(keyword.KeywordedArticles);
            }

            return foundArticles.Distinct().OrderBy(a => a.Title);
        }

        public IOrderedQueryable<NewsArticle> GetAllByAuthor(string authorUserName)
        {
            return base.GetAll().Where(a => a.ArticleAuthor.UserName == authorUserName).OrderByDescending(a => a.PostedOn);
        }

        public IOrderedQueryable<NewsArticle> GetAllHavingContent(ISet<string> freeTextSearchTerms)
        {
            char[] splitCriteria = new char[] { ' ', '\n', '\r', '\t' };

            return base.GetAll().Where(a =>
                        freeTextSearchTerms.IsProperSubsetOf(a.Content.Split(splitCriteria, StringSplitOptions.RemoveEmptyEntries))
                        ).Distinct().OrderBy(a => a.Title);
        }
    }
}

using KrastevNewsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrastevNewsSystem.Services.Contracts
{
    public interface IArticlesService : IService<NewsArticle>
    {
        //IQueryable<NewsArticle> GetAll();
        void Add(NewsArticle article, string userName);
        IOrderedEnumerable<NewsArticle> GetAllTaggedWithKeyWords(ISet<string> keywords);
        IOrderedQueryable<NewsArticle> GetAllHavingContent(ISet<string> freeTextSearchTerms);

        IOrderedQueryable<NewsArticle> GetAllByAuthor(string authorUserName);
    }
}

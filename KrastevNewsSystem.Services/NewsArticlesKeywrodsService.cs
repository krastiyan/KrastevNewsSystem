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
    class NewsArticlesKeywrodsService : BaseService<ArticleKeyword>, IArticleKeywrodsService
    {
        public NewsArticlesKeywrodsService(IKrastevNewsSystemPersister data) : base(data)
        {}

        public IOrderedEnumerable<NewsArticle> GetKeywrodTaggedArticles(string keyword)
        {
            return base.GetAll().FirstOrDefault(k => k.Keyword == keyword).KeywordedArticles.OrderByDescending(a => a.Title);
        }
    }
}

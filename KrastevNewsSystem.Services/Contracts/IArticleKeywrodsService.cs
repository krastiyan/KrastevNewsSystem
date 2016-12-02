using KrastevNewsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrastevNewsSystem.Services.Contracts
{
    public interface IArticleKeywrodsService : IService<ArticleKeyword>
    {
        //IQueryable<ArticleKeyword> GetAll();

        IOrderedEnumerable<NewsArticle> GetKeywrodTaggedArticles(string keyword);
    }
}

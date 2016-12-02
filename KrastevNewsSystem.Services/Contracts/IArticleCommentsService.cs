using KrastevNewsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrastevNewsSystem.Services.Contracts
{
    public interface IArticleCommentsService : IService<NewsArticleComment>
    {
        //IQueryable<NewsArticleComment> GetAll();

        IOrderedQueryable<NewsArticleComment> GetByCommentedArticle(int articleID);
    }
}

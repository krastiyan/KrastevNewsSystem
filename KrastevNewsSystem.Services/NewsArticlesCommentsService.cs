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
    public class NewsArticlesCommentsService : BaseService<NewsArticleComment>, IArticleCommentsService
    {
        public NewsArticlesCommentsService(IKrastevNewsSystemPersister data) : base(data)
        {}

        public IQueryable<NewsArticleComment> GetByCommentAuthor(string authorUsername)
        {
            return base.GetAll().Where(ac => ac.CommentAuthor.UserName == authorUsername);
        }

        //public IQueryable<NewsArticleComment> GetByCommentedArticle(int articleID)
        //{
        //    return base.GetAll().Where(ac => ac.CommentedNewsArticle.Id == articleID);
        //}

    }
}

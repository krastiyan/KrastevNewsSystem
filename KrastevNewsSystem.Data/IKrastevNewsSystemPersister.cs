namespace KrastevNewsSystem.Data
{
    using KrastevNewsSystem.Data.Repositories;
    using KrastevNewsSystem.Models;
    using System.Data.Entity;
    public interface IKrastevNewsSystemPersister
    {
        DbContext DataManager { get; }

        IRepository<NewsApplicationUser> Users
        {
            get;
            set;
        }

        IRepository<NewsArticle> Articles
        {
            get;
            set;
        }

        IRepository<NewsArticleComment> ArticlesComments
        {
            get;
            set;
        }

        IRepository<ArticleKeyword> ArticlesKeywords
        {
            get;
            set;
        }

        int SaveChanges();

        IRepository<T> GetRepository<T>() where T : class;

    }
}

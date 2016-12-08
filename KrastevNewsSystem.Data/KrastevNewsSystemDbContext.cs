namespace KrastevNewsSystem.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    //using KrastevNewsSystem.Data.Migrations;
    using KrastevNewsSystem.Models;

    public class KrastevNewsSystemDbContext : IdentityDbContext
    {
        public KrastevNewsSystemDbContext()
            : base("KrastevNewsSystemConnection")
        {}

        public IDbSet<NewsApplicationUser> Users
        {
            get;
            set;
        }

        public IDbSet<NewsArticle> Articles
        {
            get;
            set;
        }

        public IDbSet<NewsArticleComment> ArticlesComments
        {
            get;
            set;
        }

        public IDbSet<ArticleKeyword> ArticlesKeywords
        {
            get;
            set;
        }

        public static KrastevNewsSystemDbContext Create()
        {
            return new KrastevNewsSystemDbContext();
        }
    }
}

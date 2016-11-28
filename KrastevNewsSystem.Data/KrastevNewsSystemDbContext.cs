namespace KrastevNewsSystem.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using KrastevNewsSystem.Data.Migrations;
    using KrastevNewsSystem.Models;

    public class KrastevNewsSystemDbContext : IdentityDbContext
    {
        public KrastevNewsSystemDbContext()
            : base("KrastevNewsSystemConnection")
        {}

        //IDbSet<NewsApplicationUser> Users
        //{
        //    get;
        //    set;
        //}

        IDbSet<NewsArticle> Articles
        {
            get;
            set;
        }

        IDbSet<NewsArticleComment> ArticlesComments
        {
            get;
            set;
        }

        IDbSet<ArticleKeyword> ArticlesKeywords
        {
            get;
            set;
        }

        //IDbSet<Coordinate> Coordinates
        //{
        //    get;
        //    set;
        //}

        //IDbSet<Event> Events
        //{
        //    get;
        //    set;
        //}

        public static KrastevNewsSystemDbContext Create()
        {
            return new KrastevNewsSystemDbContext();
        }
    }
}

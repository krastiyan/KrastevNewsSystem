namespace KrastevNewsSystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using KrastevNewsSystem.Data.Repositories;
    using KrastevNewsSystem.Models;

    public class KrastevNewsSystemDataPersister : IKrastevNewsSystemPersister
    {
        private DbContext context;
        private Dictionary<Type, object> repositories;

        public KrastevNewsSystemDataPersister()
            : this(new KrastevNewsSystemDbContext())
        {
        }

        public KrastevNewsSystemDataPersister(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public DbContext DataManager
        {
            get
            {
                return this.context;
            }
        }

        public IRepository<NewsArticle> Articles
        {
            get
            {
                return this.GetRepository<NewsArticle>();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<NewsArticleComment> ArticlesComments
        {
            get
            {
                return this.GetRepository<NewsArticleComment>();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<ArticleKeyword> ArticlesKeywords
        {
            get
            {
                return this.GetRepository<ArticleKeyword>();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<NewsApplicationUser> Users
        {
            get
            {
                return this.GetRepository<NewsApplicationUser>();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(Repository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

    }
}

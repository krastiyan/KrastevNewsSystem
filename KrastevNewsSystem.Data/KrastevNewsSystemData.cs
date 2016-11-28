namespace KrastevNewsSystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using KrastevNewsSystem.Data.Repositories;
    using KrastevNewsSystem.Models;

    public class KrastevNewsSystemData : IKrastevNewsSystemData
    {
        private DbContext context;
        private Dictionary<Type, object> repositories;

        public KrastevNewsSystemData()
            : this(new KrastevNewsSystemDbContext())
        {
        }

        public KrastevNewsSystemData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<NewsApplicationUser> Users
        {
            get
            {
                return this.GetRepository<NewsApplicationUser>();
            }
        }

        //public IRepository<Teacher> Teachers
        //{
        //    get
        //    {
        //        return this.GetRepository<Teacher>();
        //    }
        //}

        //public IRepository<Student> Students
        //{
        //    get
        //    {
        //        return this.GetRepository<Student>();
        //    }
        //}

        //public IRepository<Group> Groups
        //{
        //    get
        //    {
        //        return this.GetRepository<Group>();
        //    }
        //}


        //public IRepository<Coordinate> Coordinates
        //{
        //    get
        //    {
        //        return this.GetRepository<Coordinate>();
        //    }
        //}

        //public IRepository<Event> Events
        //{
        //    get
        //    {
        //        return this.GetRepository<Event>();
        //    }
        //}

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(Repository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>) this.repositories[typeOfRepository];
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
    }
}

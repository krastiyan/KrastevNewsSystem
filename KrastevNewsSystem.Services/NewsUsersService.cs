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
    public class NewsUsersService : BaseService<NewsApplicationUser>, IUsersService
    {
        public NewsUsersService(IKrastevNewsSystemPersister data) : base(data)
        {}

        /**
         * List users with no posts and no comments in system
         **/
        public IQueryable<NewsApplicationUser> GetNonActiveUsers()
        {
            return base.GetAll().Where(u => u.Articles.Count() == 0 || u.ArticleComments.Count() == 0);
        }
    }
}

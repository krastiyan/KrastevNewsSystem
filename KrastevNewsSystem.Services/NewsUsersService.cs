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
    class NewsUsersService : BaseService<NewsApplicationUser>, IUsersService
    {
        public NewsUsersService(IKrastevNewsSystemPersister data) : base(data)
        {}

        public IQueryable<NewsApplicationUser> GetNonActiveUsers()
        {
            return base.GetAll().Where(u => u.Articles.Count() == 0 || u.ArticleComments.Count() == 0);
        }
    }
}

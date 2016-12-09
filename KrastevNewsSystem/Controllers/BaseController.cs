using KrastevNewsSystem.Data;
using KrastevNewsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace KrastevNewsSystem.Controllers
{
    public class BaseController : Controller
    {

        //public BaseController()
        //    : this(new KrastevNewsSystemDbContext())
        //{
        //}
        //public BaseController(KrastevNewsSystemDbContext context)
        //{
        //    this.PersistenceContext = context;
        //}
        //public KrastevNewsSystemDbContext PersistenceContext
        //{
        //    get;
        //    set;
        //}

        //   Services would use dataManager on thier own and Base Controller needs not to worry about that
        public BaseController(IKrastevNewsSystemPersister dataManager)
        {
            this.DataManager = dataManager;
        }

        public IKrastevNewsSystemPersister DataManager { get; set; }

        public static Expression<Func<ArticleKeyword, bool>> KeywordIsValid(DateTime compareDate)
        {
            return k => k.ValidFrom < compareDate &&
                          (k.ValidTo == null || k.ValidTo > compareDate);
        }
    }
}
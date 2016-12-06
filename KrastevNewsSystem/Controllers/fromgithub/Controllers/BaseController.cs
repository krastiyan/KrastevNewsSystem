using KrastevNewsSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public BaseController(IKrastevNewsSystemPersister dataManager)
        {
            this.DataManager = dataManager;
        }

        public IKrastevNewsSystemPersister DataManager { get; set; }
    }
}
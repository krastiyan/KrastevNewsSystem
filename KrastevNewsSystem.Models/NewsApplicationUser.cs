using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace KrastevNewsSystem.Models
{

    public class NewsApplicationUser : IdentityUser
    {

        public NewsApplicationUser()
        {
            this.Articles = new HashSet<NewsArticle>();
            this.ArticleComments = new HashSet<NewsArticleComment>();
        }

        //[Key]
        //public int Id { get; set; }
        //Above would hide inheruted automatic property Id from Identity package

        public virtual ICollection<NewsArticle> Articles { get; set; }
        public virtual ICollection<NewsArticleComment> ArticleComments { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<NewsApplicationUser> manager)
       {
           // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
           var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
           // Add custom user claims here
           return userIdentity;
       }
    }
}

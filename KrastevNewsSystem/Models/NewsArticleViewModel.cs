using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KrastevNewsSystem.Models
{
    public class NewsArticleViewModel
    {
        public int ArticleID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [Display(Name = "Posted")]
        public DateTime PostedOn { get; set; }

        [Display(Name = "Reported by")]
        public string ArticleAuthor { get; set; }

        [Display(Name = "Article Comments")]
        public ICollection<NewsArticleComment> Comments { get; set; }
    }
}
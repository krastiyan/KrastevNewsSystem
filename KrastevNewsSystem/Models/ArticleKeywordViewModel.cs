using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KrastevNewsSystem.Models
{
    public class ArticleKeywordViewModel
    {
        public int KeywordId { get; set; }
        public string Keyword { get; set; }

        public bool IsStoryKeyword { get; set; }

        [Display(Name = "Valid from")]
        public DateTime ValidFrom { get; set; }

        [Display(Name = "Valid until")]
        public DateTime? ValidTo { get; set; }

        [Display(Name = "Articles tagged")]
        public ICollection<NewsArticle> KeywordedArticles { get; set; }

        public bool IsKeywordValid { get; set; }
    }
}
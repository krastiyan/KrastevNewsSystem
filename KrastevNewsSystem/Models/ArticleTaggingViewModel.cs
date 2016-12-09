using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KrastevNewsSystem.Models
{
    public class ArticleTaggingViewModel
    {
        [Display(Name = "TaggedArticleData")]
        public NewsArticleViewModel taggedArticle { get; set; }

        [Display(Name = "ArticleTaggings")]
        public IEnumerable<ArticleKeywordViewModel> taggsList { get; set; }
    }
}
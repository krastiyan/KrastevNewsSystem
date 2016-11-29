using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KrastevNewsSystem.Models
{
    public class NewsArticleCommentViewModel
    {
        public string Content { get; set; }

        [Display(Name = "Posted")]
        public DateTime PostedOn { get; set; }

        public int CommentedNewsArticleID { get; set; }

        [Display(Name = "Commentor: ")]
        public string CommentAuthor { get; set; }
    }
}
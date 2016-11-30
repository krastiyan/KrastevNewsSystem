using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KrastevNewsSystem.Models
{
    public class NewsArticleCommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }

        [Display(Name = "Posted ")]
        public DateTime PostedOn { get; set; }

        public int CommentedNewsArticleID { get; set; }

        [Display(Name = "Reply to comment ")]
        public int CommentRepliedToID { get; set; }

        [Display(Name = "By: ")]
        public string CommentRepliedToAuthor { get; set; }

        [Display(Name = "Since: ")]
        public DateTime CommentRepliedToDate { get; set; }

        [Display(Name = "Commenter: ")]
        public string CommentAuthor { get; set; }
    }
}
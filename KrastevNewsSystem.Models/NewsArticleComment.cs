using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrastevNewsSystem.Models
{
    public class NewsArticleComment
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        public virtual NewsArticle CommentedArticle { get; set; }

        public virtual NewsApplicationUser CommentAuthor { get; set; }
    }
}

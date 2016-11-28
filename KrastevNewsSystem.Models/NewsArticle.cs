using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrastevNewsSystem.Models
{
    public class NewsArticle
    {
        public NewsArticle()
        {
            this.Comments = new HashSet<NewsArticleComment>();
            this.AssignedKeywords = new HashSet<ArticleKeyword>();
        }

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        public virtual NewsApplicationUser ArticleAuthor { get; set; }

        public virtual ICollection<NewsArticleComment> Comments { get; set; }

        public virtual ICollection<ArticleKeyword> AssignedKeywords { get; set; }
    }
}

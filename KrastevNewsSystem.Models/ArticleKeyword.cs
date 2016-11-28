using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrastevNewsSystem.Models
{
    public class ArticleKeyword
    {
        public ArticleKeyword()
        {
            this.KeywordedArticles = new HashSet<NewsArticle>();
        }

        [Key]
        public int KeywordId { get; set; }
        public string Keyword { get; set; }

        public bool IsStoryKeyword { get; set; }

        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public virtual ICollection<NewsArticle> KeywordedArticles { get; set; }
    }
}

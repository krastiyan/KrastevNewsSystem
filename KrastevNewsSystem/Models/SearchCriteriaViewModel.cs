using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KrastevNewsSystem.Models
{
    public class SearchCriteriaViewModel
    {
        [Display(Name = "Free text search criteria:")]
        public string freeTextSearchCriteria { get; set; }

        [Display (Name = "Keywords to search by:")]
        public List<SelectableKeyword> selectableKeywords { get; set; }

        public List<int> keywordsIDs { get; set; }
        public List<string> keywordsValues { get; set; }
    }

    public class SelectableKeyword
    {
        public int id { get; set; }
        public string value { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowseDotNet.Web.Models
{
    public class SearchKeyViewModel
    {
        public ICollection<SolutionViewModel> Solutions { get; set; }
        public ICollection<SnippetViewModel> Snippets { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrowseDotNet.Web.Models
{
    public class ProgrammingLanguageViewModel
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name="Language")]
        public string Name { get; set; }
        [Display(Name = "Number of Snippets")]
        public int NumberOfSnippets { get; set; }
    }
}
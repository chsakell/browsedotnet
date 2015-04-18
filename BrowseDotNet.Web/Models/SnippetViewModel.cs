using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrowseDotNet.Web.Models
{
    public class SnippetViewModel
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Code { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Group")]
        public string GroupName { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        [MaxLength(200)]
        [DataType(DataType.Url)]
        public string Website { get; set; }

        [Display(Name = "Language")]
        public int ProgrammingLanguageID { get; set; }

        [Display(Name = "Language")]
        public string ProgrammingLanguageName { get; set; }

        [Required]
        [MaxLength(200)]
        [DataType(DataType.MultilineText)]
        public string Keys { get; set; }
    }
}
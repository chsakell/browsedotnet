using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrowseDotNet.Web.Models
{
    public class SolutionTypeViewModel
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Display(Name = "Number of Solutions")]
        public int NumberOfSolutions { get; set; }
    }
}
using BrowseDotNet.Web.Models.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrowseDotNet.Web.Models
{
    public class SolutionViewModel : IValidatableObject
    {
        public int ID { get; set; }

        [MaxLength(50)]
        public string Author { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Application")]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        [Display(Name = "Full Path (.sln)")]
        [RegularExpression(@"^(?:[\w]\:|\\)(\\[a-zA-Z_\-\s0-9\.]+)+\.(sln)$", ErrorMessage = "Invalid file path format.")]
        public string FilePath { get; set; }

        [Required]
        [MaxLength(300)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [MaxLength(200)]
        [DataType(DataType.Url)]
        public string Website { get; set; }

        [Required]
        [Display(Name = "Type")]
        public int SolutionTypeID { get; set; }

        [Display(Name = "Type")]
        public string SolutionTypeType { get; set; }

        [Required]
        [MaxLength(200)]
        [DataType(DataType.MultilineText)]
        public string Keys { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new SolutionViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
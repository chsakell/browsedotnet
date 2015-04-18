using BrowseDotNet.Web.Models.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BrowseDotNet.Web.Models
{
    public class RootDirectoryViewModel : IValidatableObject
    {
        [Required]
        [MaxLength(250)]
        [Display(Name = "Root directory")]
        [RegularExpression(@"^[a-zA-Z]+:+(\\)+([a-zA-Z]*(\\)+)+", ErrorMessage = "Invalid directory path format.")]
        public string RootDirectoryPath { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new RootDirectoryViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
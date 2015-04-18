using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BrowseDotNet.Web.Models.Validators
{
    public class SolutionViewModelValidator : AbstractValidator<SolutionViewModel>
    {
        public SolutionViewModelValidator()
        {
            RuleFor(s => s.FilePath)
                .Must(IsValidPath)
                .WithMessage("Enter solution's full file path.");
        }

        private bool IsValidPath(string filePath)
        {
            bool _isValidSolutionPath = new bool();

            try
            {

                if (File.Exists(filePath) && Path.GetExtension(filePath).EndsWith(".sln"))
                    _isValidSolutionPath = true;
            }
            catch 
            {
                _isValidSolutionPath = false;
            }

            return _isValidSolutionPath;
        }
    }
}
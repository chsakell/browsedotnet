using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BrowseDotNet.Web.Models.Validators
{
    public class RootDirectoryViewModelValidator : AbstractValidator<RootDirectoryViewModel>
    {
        public RootDirectoryViewModelValidator()
        {
            RuleFor(s => s.RootDirectoryPath)
                .Must(IsValidPath)
                .WithMessage("Enter a valid root directory path.");
        }

        private bool IsValidPath(string filePath)
        {
            bool _isValidSolutionPath = new bool();

            try
            {

                if (Directory.Exists(filePath))
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
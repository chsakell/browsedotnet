using AutoMapper;
using BrowseDotNet.Domain;
using BrowseDotNet.Service;
using BrowseDotNet.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrowseDotNet.Web.Infrastructure.Alerts;

namespace BrowseDotNet.Web.Controllers
{
    public class SnippetsController : Controller
    {
        private readonly ISnippetService _snippetService;
        private readonly IUtilityService _utilityService;
        private readonly ISearchKeyService _searchKeyService;
        private readonly IProgrammingLanguageService _languageService;

        public SnippetsController(ISnippetService snippetService, IUtilityService utilityService,
            ISearchKeyService searchKeyService, IProgrammingLanguageService languageService)
        {
            _snippetService = snippetService;
            _searchKeyService = searchKeyService;
            _languageService = languageService;
            _utilityService = utilityService;
        }

        public ActionResult Index()
        {
            var snippets = _snippetService.GetSnippets();
            var snippetsVM = Mapper.Map<IEnumerable<Snippet>, IEnumerable<SnippetViewModel>>(snippets);

            return View(snippetsVM);
        }

        public ActionResult Edit(int id)
        {
            var snippet = _snippetService.GetSnippet(id);
            var snippetVM = Mapper.Map<Snippet, SnippetViewModel>(snippet);

            SetLanguageTypes(snippetVM);
            return View(snippetVM);
        }

        public JsonResult SearchGroup(string term)
        {
            List<string> availableGroups = _snippetService.GetSnippets().Select(s => s.GroupName).Distinct().ToList();

            List<string> matchedGroups = availableGroups.FindAll(x => x.StartsWith(term, StringComparison.OrdinalIgnoreCase));

            return Json(matchedGroups, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(SnippetViewModel snippet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Snippet editedSnippet = _snippetService.GetSnippet(snippet.ID);
                    editedSnippet.Name = snippet.Name.Trim();
                    editedSnippet.Description = snippet.Description.Trim();
                    editedSnippet.GroupName = snippet.GroupName.Trim();
                    editedSnippet.ProgrammingLanguageID = snippet.ProgrammingLanguageID;
                    editedSnippet.Keys.Clear();
                    editedSnippet.Keys = _utilityService.ResolveKeys(snippet.Keys);
                    editedSnippet.Website = snippet.Website;
                    editedSnippet.Code = snippet.Code;

                    try
                    {
                        _snippetService.SaveSnippet();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                        // Join the list to a single string.
                        var fullErrorMessage = string.Join("; ", errorMessages);

                        // Combine the original exception message with the new one.
                        var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                        // Throw a new DbEntityValidationException with the improved exception message.
                        throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                    }

                    SetLanguageTypes(snippet);

                    return View(snippet).WithSuccess(snippet.Name + " succesfully updated.");
                }
                else
                {
                    ModelState.AddModelError("", "Validation errors occured.");

                    SetLanguageTypes(snippet);
                    return View(snippet);
                }
            }
            catch (Exception ex)
            {
                SetLanguageTypes(snippet);
                return View(snippet).WithError(ex.Message);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int selectedSnippetID)
        {
            string _returnMessage = string.Empty;
            bool _error = new bool();

            try
            {
                var selectedSnippet = _snippetService.GetSnippet(selectedSnippetID);
                _snippetService.RemoveSnippet(selectedSnippetID);
                _snippetService.SaveSnippet();

                _returnMessage = selectedSnippet.Name + " removed successfully from BrowseDotNET.";

            }
            catch (Exception ex)
            {
                _returnMessage = "Error trying to remove snippet from BrowseDotNET." + ex.Message;
                _error = true;
            }

            if (_error)
                return RedirectToAction("Index").WithError(_returnMessage);
            else
                return RedirectToAction("Index").WithSuccess(_returnMessage);
        }

        public ActionResult Code(int id)
        {
            var snippet = _snippetService.GetSnippet(id);
            var snippetVM = Mapper.Map<Snippet, SnippetViewModel>(snippet);

            return View(snippetVM);
        }

        public ActionResult Group(string group)
        {
            var snippets = _snippetService.GetSnippets()
                .Where(s => s.GroupName.ToLower().Contains(group.ToLower().Trim()));

            var snippetsVM = Mapper.Map<IEnumerable<Snippet>, IEnumerable<SnippetViewModel>>(snippets);
            ViewBag.GroupTerm = group;

            return View(snippetsVM);
        }

        #region Methods

        private void SetLanguageTypes(SnippetViewModel selectedSnippet)
        {
            var languageTypes = _languageService.GetProgrammingLanguages();

            ViewBag.LanguageID = new SelectList(languageTypes,
                "ID", "Name", selectedSnippet.ProgrammingLanguageID);

            if (string.IsNullOrEmpty(selectedSnippet.ProgrammingLanguageName))
            {
                selectedSnippet.ProgrammingLanguageName = languageTypes
                    .FirstOrDefault(t => t.ID == selectedSnippet.ProgrammingLanguageID).Name;
            }

            ViewBag.SelectedLanguage = selectedSnippet;
        }

        #endregion
    }
}
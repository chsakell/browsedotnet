using BrowseDotNet.Service;
using BrowseDotNet.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrowseDotNet.Web.Infrastructure.Alerts;
using BrowseDotNet.Domain;
using System.Data.Entity.Validation;
using System.Web.Script.Serialization;
using System.IO;
using BrowseDotNet.Web.Models.jsTree;

namespace BrowseDotNet.Web.Controllers
{
    [Authorize]
    public class RegisterController : Controller
    {
        #region Interfaces
        private readonly ISolutionService _solutionService;
        private readonly ISnippetService _snippetService;
        private readonly ISolutionTypeService _solutionTypeService;
        private readonly ISearchKeyService _searchKeyService;
        private readonly IUtilityService _utilityService;
        private readonly IProgrammingLanguageService _languageService;
        #endregion

        public RegisterController(ISolutionService solutionService, ISolutionTypeService solutionTypeService,
            ISearchKeyService searchKeyService, ISnippetService snippetService,
            IProgrammingLanguageService languageService, IUtilityService utilityService)
        {
            _solutionService = solutionService;
            _snippetService = snippetService;
            _solutionTypeService = solutionTypeService;
            _searchKeyService = searchKeyService;
            _utilityService = utilityService;
            _languageService = languageService;
        }


        #region Solutions

        public ActionResult DotNetSolution()
        {
            SetSolutionTypes();
            return View();
        }

        public ActionResult ScanSolutions()
        {
            return View();
        }

        public ActionResult Directories(string rootDirectory)
        {
            var model = GetTreeData(rootDirectory);

            string jsonModel = new JavaScriptSerializer().Serialize(model);

            return View("Directories", "_Layout", jsonModel);
        }

        [HttpPost]
        public ActionResult ScanDirectories(FormCollection form)
        {
            int _registeredSolutions = 0;
            try
            {
                string[] directoriesChecked = form.AllKeys;
                List<string> directoriesToScan = _utilityService.ResolveDirectories(directoriesChecked);
                if (directoriesToScan != null)
                    foreach (string directory in directoriesToScan)
                    {
                        string _solutionFile = string.Empty;
                        if (_utilityService.HasDirectoryDotNETSolution(directory, out _solutionFile))
                        {
                            Solution _newSolution = new Solution();
                            _newSolution.Name = _solutionFile;
                            _newSolution.Description = _solutionFile;
                            _newSolution.DateRegistered = DateTime.Now;
                            _newSolution.FilePath = Path.Combine(directory, _solutionFile + ".sln");
                            _newSolution.SolutionTypeID = _solutionTypeService.GetDefaultSolutionTypeID();
                            _solutionService.CreateSolution(_newSolution);
                            try
                            {
                                _solutionService.SaveSolution();
                                _registeredSolutions++;
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }

                return RedirectToAction("Index", "Solutions").WithSuccess(_registeredSolutions + " found and registered automatically by BrowseDotNET.");
            }
            catch (Exception ex)
            {
                return RedirectToAction("ScanSolutions").WithError("Errors occured during scanning for .NET Solutions." + ex.Message);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ScanSolutions(RootDirectoryViewModel directory)
        {

            if (ModelState.IsValid)
            {

                return RedirectToAction("Directories", new { rootDirectory = directory.RootDirectoryPath })
                    .WithSuccess("Select Directories to be scanned.");
            }
            else
            {
                return View().WithError("Invalid root directory.");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DotNetSolution(SolutionViewModel solution)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Solution newSolution = new Solution();
                    newSolution.Author = solution.Author;
                    newSolution.Description = solution.Description;
                    newSolution.FilePath = solution.FilePath.Trim();
                    newSolution.Name = solution.Name.Trim();
                    newSolution.SolutionTypeID = solution.SolutionTypeID;
                    newSolution.Keys = _utilityService.ResolveKeys(solution.Keys);
                    newSolution.Website = solution.Website;
                    newSolution.DateRegistered = DateTime.Now;

                    try
                    {
                        _solutionService.CreateSolution(newSolution);
                        _solutionService.SaveSolution();
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

                    SetSolutionTypes();
                    return RedirectToAction("DotNetSolution").WithSuccess(solution.Name + " succesfully added.");
                }
                else
                {
                    ModelState.AddModelError("", "Validation errors occured.");

                    SetSolutionTypes();
                    return View(solution);
                }
            }
            catch (Exception ex)
            {
                SetSolutionTypes();
                return View(solution).WithError(ex.Message);
            }
        }


        #endregion

        #region Snippets

        public ActionResult CodeSnippet()
        {
            SetLanguagesTypes();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CodeSnippet(SnippetViewModel snippet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Snippet newSnippet = new Snippet();
                    newSnippet.Name = snippet.Name.Trim();
                    newSnippet.Description = snippet.Description.Trim();
                    newSnippet.GroupName = snippet.GroupName.ToLower().Trim();
                    newSnippet.Keys = _utilityService.ResolveKeys(snippet.Keys);
                    newSnippet.DateCreated = DateTime.Now;
                    if (!string.IsNullOrEmpty(snippet.Website))
                        newSnippet.Website = snippet.Website.Trim();
                    newSnippet.Code = snippet.Code;
                    newSnippet.ProgrammingLanguageID = snippet.ProgrammingLanguageID;

                    try
                    {
                        _snippetService.CreateSnippet(newSnippet);
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

                    SetLanguagesTypes();
                    return RedirectToAction("Code", "Snippets", new { id = newSnippet.ID })
                        .WithSuccess(newSnippet.Name + " saved successfully.");
                }
                else
                {
                    ModelState.AddModelError("", "Validation errors occured.");

                    SetLanguagesTypes();
                    return View(snippet);
                }
            }
            catch (Exception ex)
            {
                SetLanguagesTypes();
                return View(snippet).WithError(ex.Message);
            }
        }

        public JsonResult SearchGroup(string term)
        {
            List<string> availableGroups = _snippetService.GetSnippets().Select(s => s.GroupName).Distinct().ToList();

            List<string> matchedGroups = availableGroups.FindAll(x => x.StartsWith(term, StringComparison.OrdinalIgnoreCase));

            return Json(matchedGroups, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Methods

        private void SetSolutionTypes()
        {
            var solutionTypes = _solutionTypeService.GetSolutionTypes();
            var firstSolutionType = solutionTypes.First();

            ViewBag.SolutionTypeID = new SelectList(solutionTypes,
                "ID", "Type", firstSolutionType.ID);

            ViewBag.SelectedType = firstSolutionType;
        }

        private void SetLanguagesTypes()
        {
            var languages = _languageService.GetProgrammingLanguages();
            var firstlanguage = languages.First();

            ViewBag.LanguageID = new SelectList(languages,
                "ID", "Name", firstlanguage.ID);

            ViewBag.SelectedLanguage = firstlanguage;
        }

        #region jsTree

        private JsTreeModel GetTreeData(string rootDirectory)
        {
            JsTreeModel rootNode = new JsTreeModel();
            rootNode.attr = new JsTreeAttribute();
            rootNode.data = "ROOT";
            string rootPath = rootDirectory;// Request.MapPath("/Root");
            rootNode.attr.id = rootPath;
            PopulateTree(rootPath, rootNode);

            return rootNode;
        }

        public void PopulateTree(string dir, JsTreeModel node)
        {
            if (node.children == null)
            {
                node.children = new List<JsTreeModel>();
            }
            // get the information of the directory
            DirectoryInfo directory = new DirectoryInfo(dir);
            // loop through each subdirectory
            foreach (DirectoryInfo d in directory.GetDirectories())
            {
                // create a new node
                JsTreeModel t = new JsTreeModel();
                t.attr = new JsTreeAttribute();
                t.attr.id = d.FullName;
                t.data = d.Name.ToString();
                // populate the new node recursively
                PopulateTree(d.FullName, t);
                node.children.Add(t); // add the node to the "master" node
            }
            // lastly, loop through each file in the directory, and add these as nodes
            //foreach (FileInfo f in directory.GetFiles())
            //{
            //    // create a new node
            //    JsTreeModel t = new JsTreeModel();
            //    t.attr = new JsTreeAttribute();
            //    t.attr.id = f.FullName;
            //    t.data = f.Name.ToString();
            //    // add it to the "master"
            //    node.children.Add(t);
            //}
        }

        #endregion

        #endregion
    }
}
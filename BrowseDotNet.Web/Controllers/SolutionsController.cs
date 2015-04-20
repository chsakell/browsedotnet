using AutoMapper;
using BrowseDotNet.Domain;
using BrowseDotNet.Service;
using BrowseDotNet.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrowseDotNet.Web.Infrastructure.Alerts;
using System.Data.Entity.Validation;

namespace BrowseDotNet.Web.Controllers
{
    [Authorize]
    public class SolutionsController : Controller
    {
        private readonly ISolutionService _solutionService;
        private readonly IUtilityService _utilityService;
        private readonly ISolutionTypeService _solutionTypeService;
        private readonly ISearchKeyService _searchKeyService;

        public SolutionsController(ISolutionService solutionService, IUtilityService utilityService,
            ISolutionTypeService solutionTypeService, 
            ISearchKeyService searchKeyService)
        {
            _solutionService = solutionService;
            _solutionTypeService = solutionTypeService;
            _searchKeyService = searchKeyService;
            _utilityService = utilityService;
        }

        // GET: Solutions
        public ActionResult Index()
        {
            var solutions = _solutionService.GetSolutions();
            var solutionsVM = Mapper.Map<IEnumerable<Solution>, IEnumerable<SolutionViewModel>>(solutions);

            return View(solutionsVM);
        }

        public ActionResult Open(int ID)
        {
            bool isAdmin = new bool();
            if (Request.QueryString["IsAdmin"] != null)
                bool.TryParse(Request.QueryString["IsAdmin"], out isAdmin);
            try
            {
                var solution = _solutionService.GetSolution(ID);
                if (solution != null)
                {
                    _utilityService.OpenDotNETSolution(solution.FilePath, isAdmin);
                }

                try
                {
                    solution.LastTimeOpened = DateTime.Now;
                    _solutionService.SaveSolution();
                }
                catch
                { }

                return RedirectToAction("Index").WithSuccess(solution.Name + " is being opening.. Happy coding!");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index").WithError("Error trying to open selected solution. " + ex.Message);
            }
        }

        public ActionResult Edit(int id)
        {
            var solution = _solutionService.GetSolution(id);
            var solutionVM = Mapper.Map<Solution, SolutionViewModel>(solution);

            SetSolutionTypes(solutionVM);

            return View(solutionVM);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(SolutionViewModel solution)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Solution editedSolution = _solutionService.GetSolution(solution.ID);
                    editedSolution.Author = solution.Author;
                    editedSolution.Description = solution.Description;
                    editedSolution.FilePath = solution.FilePath.Trim();
                    editedSolution.Name = solution.Name.Trim();
                    editedSolution.SolutionTypeID = solution.SolutionTypeID;
                    editedSolution.Keys.Clear();
                    editedSolution.Keys = _utilityService.ResolveKeys(solution.Keys); // TODO: Update keys
                    editedSolution.Website = solution.Website;
                    //editedSolution.DateRegistered = DateTime.Now; // TODO: Dateupdated

                    try
                    {
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

                    SetSolutionTypes(solution);

                    return View(solution).WithSuccess(solution.Name + " succesfully updated.");
                }
                else
                {
                    ModelState.AddModelError("", "Validation errors occured.");

                    SetSolutionTypes(solution);
                    return View(solution);
                }
            }
            catch (Exception ex)
            {
                SetSolutionTypes(solution);
                return View(solution).WithError(ex.Message);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int selectedSolutionID)
        {
            string _returnMessage = string.Empty;
            bool _error = new bool();

            try
            {
                var selectedSolution = _solutionService.GetSolution(selectedSolutionID);
                _solutionService.RemoveSolution(selectedSolutionID);
                _solutionService.SaveSolution();

                _returnMessage = selectedSolution.Name + " removed successfully from BrowseDotNET.";

            }
            catch (Exception ex)
            {
                _returnMessage = "Error trying to remove solution from BrowseDotNET." + ex.Message;
                _error = true;
            }

            if (_error)
                return RedirectToAction("Index").WithError(_returnMessage);
            else
                return RedirectToAction("Index").WithSuccess(_returnMessage);
        }

        #region Methods

        private void SetSolutionTypes(SolutionViewModel selectedSolution)
        {
            var solutionTypes = _solutionTypeService.GetSolutionTypes();

            ViewBag.SolutionTypeID = new SelectList(solutionTypes,
                "ID", "Type", selectedSolution.SolutionTypeID);

            if(string.IsNullOrEmpty(selectedSolution.SolutionTypeType))
            {
                selectedSolution.SolutionTypeType = solutionTypes
                    .FirstOrDefault(t => t.ID == selectedSolution.SolutionTypeID).Type;
            }

            ViewBag.SelectedType = selectedSolution;
        }

        #endregion
    }
}
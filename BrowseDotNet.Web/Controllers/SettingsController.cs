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

namespace BrowseDotNet.Web.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IProgrammingLanguageService _languageService;
        private readonly ISolutionTypeService _solutionTypeService;

        public SettingsController(IProgrammingLanguageService languageService,ISolutionTypeService solutionTypeService)
        {
            _languageService = languageService;
            _solutionTypeService = solutionTypeService;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region Languages

        public ActionResult Languages()
        {

            var languages = _languageService.GetProgrammingLanguages();
            var languagesVM = Mapper.Map<IEnumerable<ProgrammingLanguage>,
                IEnumerable<ProgrammingLanguageViewModel>>(languages);

            return View(languagesVM);
        }

        public ActionResult CreateLanguage()
        {
            return View();
        }

        public ActionResult EditLanguage(int id)
        {
            var language = _languageService.GetProgrammingLanguage(id);
            var languageVM = Mapper.Map<ProgrammingLanguage, ProgrammingLanguageViewModel>(language);

            return View(languageVM);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditLanguage(ProgrammingLanguageViewModel language)
        {
            if (ModelState.IsValid)
            {
                ProgrammingLanguage lang = _languageService.GetProgrammingLanguage(language.ID);
                lang.Name = language.Name.Trim();

                try
                {
                    _languageService.SaveProgrammingLanguage();
                }
                catch (Exception ex)
                {
                    return View(language).WithError(ex.Message);
                }

            }
            else
                return View(language).WithError("Invalid Data");

            return RedirectToAction("Languages").WithSuccess("Language " + language.Name + " updated successfully.");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateLanguage(ProgrammingLanguageViewModel language)
        {
            if(ModelState.IsValid)
            {
                ProgrammingLanguage newLanguage = new ProgrammingLanguage();
                newLanguage.Name = language.Name.Trim();
                newLanguage.DateCreated = DateTime.Now;
                try
                {
                    _languageService.CreateProgrammingLanguage(newLanguage);
                    _languageService.SaveProgrammingLanguage();
                }
                catch(Exception ex)
                {
                    return View(language).WithError(ex.Message);
                }

            }
            else
                return View(language).WithError("Invalid Language");

            return RedirectToAction("Languages").WithSuccess("Language " + language.Name + " created successfully.");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteLanguage(int programmingLanguageID)
        {
            try
            {
                var language = _languageService.GetProgrammingLanguage(programmingLanguageID);

                if (language != null)
                {
                    if (language.Snippets.Count > 0)
                        return RedirectToAction("Languages").WithError("Language " + language.Name + " has attached snippets. Remove snippets first.");
                    else
                    {
                        _languageService.RemoveProgrammingLanguage(programmingLanguageID);
                        _languageService.SaveProgrammingLanguage();
                    }
                }
                else
                    return RedirectToAction("Languages").WithError("Invalid language");

                return RedirectToAction("Languages").WithSuccess("Language " + language.Name + " removed successfully.");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Languages").WithError(ex.Message);
            }
        }

        #endregion

        #region Solution Types

        public ActionResult SolutionTypes()
        {
            var solutionTypes = _solutionTypeService.GetSolutionTypes();
            var solutionTypesVM = Mapper.Map<IEnumerable<SolutionType>,
                IEnumerable<SolutionTypeViewModel>>(solutionTypes);

            return View(solutionTypesVM);
        }

        public ActionResult CreateSolutionType()
        {
            return View();
        }

        public ActionResult EditSolutionType(int id)
        {
            var solutionType = _solutionTypeService.GetSolutionType(id);
            var solutionTypeVM = Mapper.Map<SolutionType, SolutionTypeViewModel>(solutionType);

            return View(solutionTypeVM);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateSolutionType(SolutionTypeViewModel solutionType)
        {
            if (ModelState.IsValid)
            {
                SolutionType newType = new SolutionType();
                newType.Type = solutionType.Type.Trim();
                newType.DateCreated = DateTime.Now;
                try
                {
                    _solutionTypeService.CreateSolutionType(newType);
                    _solutionTypeService.SaveSolutionType();
                }
                catch (Exception ex)
                {
                    return View(solutionType).WithError(ex.Message);
                }

            }
            else
                return View(solutionType).WithError("Invalid data");

            return RedirectToAction("SolutionTypes").WithSuccess("Solution type" + solutionType.Type + " created successfully.");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditSolutionType(SolutionTypeViewModel solutionType)
        {
            if (ModelState.IsValid)
            {
                SolutionType type = _solutionTypeService.GetSolutionType(solutionType.ID);
                type.Type = solutionType.Type.Trim();

                try
                {
                    _solutionTypeService.SaveSolutionType();
                }
                catch (Exception ex)
                {
                    return View(solutionType).WithError(ex.Message);
                }

            }
            else
                return View(solutionType).WithError("Invalid Data");

            return RedirectToAction("SolutionTypes").WithSuccess("Solution type " + solutionType.Type + " updated successfully.");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteSolutionType(int solutionTypeID)
        {
            try
            {
                var solution = _solutionTypeService.GetSolutionType(solutionTypeID);

                if (solution != null)
                {
                    if (solution.Solutions.Count > 0)
                        return RedirectToAction("SolutionTypes").WithError("Solution Type " + solution.Type + " has attached solutions. Remove solutions first.");
                    else
                    {
                        _solutionTypeService.RemoveSolutionType(solutionTypeID);
                        _solutionTypeService.SaveSolutionType();
                    }
                }
                else
                    return RedirectToAction("SolutionTypes").WithError("Invalid solution type");

                return RedirectToAction("SolutionTypes").WithSuccess("Solution type " + solution.Type + " removed successfully.");
            }
            catch (Exception ex)
            {
                return RedirectToAction("SolutionTypes").WithError(ex.Message);
            }
        }

        #endregion
    }
}
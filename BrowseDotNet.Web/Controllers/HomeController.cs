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
    public class HomeController : Controller
    {

        #region Interfaces
        private readonly ISolutionService _solutionService;
        #endregion

        public HomeController(ISolutionService solutionService)
        {
            _solutionService = solutionService;
        }
        
        // Starting page
        public ActionResult Index()
        {
            try
            {
                var latestSolutions = _solutionService.GetSolutions()
                    .OrderByDescending(s => s.LastTimeOpened).Take(6);

                var latestSolutionsVM = Mapper.Map<IEnumerable<Solution>, IEnumerable<SolutionViewModel>>(latestSolutions);

                return View(latestSolutionsVM);
            }
            catch(Exception ex)
            {
                return View().WithError(ex.Message); ;
            }
        }

        public ActionResult FAQ()
        {
            return View();
        }
    }
}
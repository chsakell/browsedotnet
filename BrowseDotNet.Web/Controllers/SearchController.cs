using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrowseDotNet.Web.Infrastructure.Alerts;
using BrowseDotNet.Service;
using AutoMapper;
using BrowseDotNet.Domain;
using BrowseDotNet.Web.Models;

namespace BrowseDotNet.Web.Controllers
{
    public class SearchController : Controller
    {

        private readonly ISolutionService _solutionService;
        private readonly IUtilityService _utilityService;
        private readonly ISolutionTypeService _solutionTypeService;
        private readonly ISearchKeyService _searchKeyService;

        public SearchController(ISolutionService solutionService, IUtilityService utilityService,
            ISolutionTypeService solutionTypeService,
            ISearchKeyService searchKeyService)
        {
            _solutionService = solutionService;
            _solutionTypeService = solutionTypeService;
            _searchKeyService = searchKeyService;
            _utilityService = utilityService;
        }
        // GET: Search
        public ActionResult Index(string searchquery)
        {
            if (string.IsNullOrEmpty(searchquery.Trim()))
            {
                return RedirectToAction("Index", "Home").WithError("Enter valid key terms");
            }
            else
            {
                try
                {
                    ViewBag.SearchQuery = searchquery;
                    searchquery = searchquery.Replace(" ", ",");
                    string[] searchTerms = searchquery.Trim().Split(',');
                    List<SearchKey> searchKeysList = new List<SearchKey>();
                    List<Solution> _solutionsFound = new List<Solution>();
                    List<Snippet> _snippetsFound = new List<Snippet>();

                    foreach (string term in searchTerms)
                    {
                        var searchKeys = _searchKeyService.GetSearchKeys(key => key.Term.ToLower()
                        .Contains(term.Trim().ToLower()));

                        if (searchKeys != null)
                        {
                            foreach (var searchKey in searchKeys)
                            {
                                if (!searchKeysList.Contains(searchKey))
                                    searchKeysList.Add(searchKey);
                            }
                        }
                    }

                    foreach (var searchKey in searchKeysList)
                    {
                        foreach (var solution in searchKey.Solutions)
                        {
                            if (!_solutionsFound.Contains(solution))
                            {
                                _solutionsFound.Add(solution);
                            }
                        }

                        foreach (var snippet in searchKey.Snippets)
                        {
                            if (!_snippetsFound.Contains(snippet))
                            {
                                _snippetsFound.Add(snippet);
                            }
                        }
                    }

                    SearchKeyViewModel searchKeysResults = new SearchKeyViewModel()
                    {
                        Solutions = Mapper.Map<List<Solution>, List<SolutionViewModel>>(_solutionsFound),
                        Snippets = Mapper.Map<List<Snippet>, List<SnippetViewModel>>(_snippetsFound)
                    };

                    return View(searchKeysResults);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", "Home").WithError(ex.Message);
                }
            }
        }
    }
}
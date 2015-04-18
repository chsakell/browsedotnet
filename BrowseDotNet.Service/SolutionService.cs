using BrowseDotNet.Data.Infrastructure;
using BrowseDotNet.Data.Repositories;
using BrowseDotNet.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Service
{
    // operations you want to expose
    public interface ISolutionService
    {
        IEnumerable<Solution> GetSolutions();
        Solution GetSolution(int id);
        void CreateSolution(Solution solution);
        void SaveSolution();
        void RemoveSolution(int id);
    }

    public class SolutionService : ISolutionService
    {
        private readonly ISolutionRepository solutionsRepository;
        private readonly IUnitOfWork unitOfWork;

        public SolutionService(ISolutionRepository solutionsRepository, IUnitOfWork unitOfWork)
        {
            this.solutionsRepository = solutionsRepository;
            this.unitOfWork = unitOfWork;
        }

        #region ISolutionService Members

        public IEnumerable<Solution> GetSolutions()
        {
            var solutions = solutionsRepository.GetAll();
            return solutions;
        }

        public Solution GetSolution(int id)
        {
            var solution = solutionsRepository.GetById(id);
            return solution;
        }

        public void CreateSolution(Solution solution)
        {
            solutionsRepository.Add(solution);
        }

        public void RemoveSolution(int id)
        {
            var solution = solutionsRepository.GetById(id);
            solutionsRepository.Delete(solution);
        }

        public void SaveSolution()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}

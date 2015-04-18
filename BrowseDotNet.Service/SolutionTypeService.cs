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
    public interface ISolutionTypeService
    {
        IEnumerable<SolutionType> GetSolutionTypes();
        SolutionType GetSolutionType(int id);
        void CreateSolutionType(SolutionType solutionType);
        void SaveSolutionType();
        int GetDefaultSolutionTypeID();
        void RemoveSolutionType(int id);
    }

    public class SolutionTypeService : ISolutionTypeService
    {
        private readonly ISolutionTypeRepository solutionsTypeRepository;
        private readonly IUnitOfWork unitOfWork;

        public SolutionTypeService(ISolutionTypeRepository solutionsTypeRepository, IUnitOfWork unitOfWork)
        {
            this.solutionsTypeRepository = solutionsTypeRepository;
            this.unitOfWork = unitOfWork;
        }

        #region ISolutionTypeService Members

        public IEnumerable<SolutionType> GetSolutionTypes()
        {
            var solutionTypes = solutionsTypeRepository.GetAll();
            return solutionTypes;
        }

        public int GetDefaultSolutionTypeID()
        {
            return GetSolutionTypes().Where(t => t.Type.ToLower().Contains("unknown")).Select(t => t.ID).FirstOrDefault();
        }

        public SolutionType GetSolutionType(int id)
        {
            var solutionType = solutionsTypeRepository.GetById(id);
            return solutionType;
        }

        public void CreateSolutionType(SolutionType solutionType)
        {
            solutionsTypeRepository.Add(solutionType);
        }

        public void RemoveSolutionType(int id)
        {
            var solutionType = solutionsTypeRepository.GetById(id);
            solutionsTypeRepository.Delete(solutionType);
        }

        public void SaveSolutionType()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}

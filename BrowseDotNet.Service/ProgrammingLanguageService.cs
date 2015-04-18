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
    public interface IProgrammingLanguageService
    {
        IEnumerable<ProgrammingLanguage> GetProgrammingLanguages();
        ProgrammingLanguage GetProgrammingLanguage(int id);
        void CreateProgrammingLanguage(ProgrammingLanguage programmingLanguage);
        void SaveProgrammingLanguage();
        int GetDefaultProgrammingLanguageID();
        void RemoveProgrammingLanguage(int id);
    }

    public class ProgrammingLanguageService : IProgrammingLanguageService
    {
        private readonly IProgrammingLanguageRepository programmingLanguageRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProgrammingLanguageService(IProgrammingLanguageRepository programmingLanguageRepository, IUnitOfWork unitOfWork)
        {
            this.programmingLanguageRepository = programmingLanguageRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IProgrammingLanguageService Members

        public IEnumerable<ProgrammingLanguage> GetProgrammingLanguages()
        {
            var programmingLanguages = programmingLanguageRepository.GetAll();
            return programmingLanguages;
        }

        public int GetDefaultProgrammingLanguageID()
        {
            return GetProgrammingLanguages().Where(t => t.Name.ToLower().Contains("unknown")).Select(t => t.ID).FirstOrDefault();
        }

        public ProgrammingLanguage GetProgrammingLanguage(int id)
        {
            var programmingLanguage = programmingLanguageRepository.GetById(id);
            return programmingLanguage;
        }

        public void CreateProgrammingLanguage(ProgrammingLanguage programmingLanguage)
        {
            programmingLanguageRepository.Add(programmingLanguage);
        }

        public void RemoveProgrammingLanguage(int id)
        {
            var language = programmingLanguageRepository.GetById(id);
            programmingLanguageRepository.Delete(language);
        }

        public void SaveProgrammingLanguage()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}

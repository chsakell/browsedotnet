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
    public interface ISnippetService
    {
        IEnumerable<Snippet> GetSnippets();
        Snippet GetSnippet(int id);
        void CreateSnippet(Snippet snippet);
        void SaveSnippet();
        void RemoveSnippet(int id);
    }

    public class SnippetService : ISnippetService
    {
        private readonly ISnippetRepository snippetsRepository;
        private readonly IUnitOfWork unitOfWork;

        public SnippetService(ISnippetRepository snippetsRepository, IUnitOfWork unitOfWork)
        {
            this.snippetsRepository = snippetsRepository;
            this.unitOfWork = unitOfWork;
        }

        #region ISnippetService Members

        public IEnumerable<Snippet> GetSnippets()
        {
            var snippets = snippetsRepository.GetAll();
            return snippets;
        }

        public Snippet GetSnippet(int id)
        {
            var snippet = snippetsRepository.GetById(id);
            return snippet;
        }

        public void CreateSnippet(Snippet snippet)
        {
            snippetsRepository.Add(snippet);
        }

        public void RemoveSnippet(int id)
        {
            var snippet = snippetsRepository.GetById(id);
            snippetsRepository.Delete(snippet);
        }

        public void SaveSnippet()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}

using BrowseDotNet.Data.Infrastructure;
using BrowseDotNet.Data.Repositories;
using BrowseDotNet.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Service
{
    // operations you want to expose
    public interface ISearchKeyService
    {
        IEnumerable<SearchKey> GetSearchKeys();
        IEnumerable<SearchKey> GetSearchKeys(Expression<Func<SearchKey, bool>> where);
        SearchKey GetSearchKey(int id);
        void CreateSearchKey(SearchKey searchKey);
        void SaveSearchKey();
    }

    public class SearchKeyService : ISearchKeyService
    {
        private readonly ISearchKeyRepository searchKeysRepository;
        private readonly IUnitOfWork unitOfWork;

        public SearchKeyService(ISearchKeyRepository searchKeysRepository, IUnitOfWork unitOfWork)
        {
            this.searchKeysRepository = searchKeysRepository;
            this.unitOfWork = unitOfWork;
        }

        #region ISearchKeyService Members

        public IEnumerable<SearchKey> GetSearchKeys()
        {
            var searchKeys = searchKeysRepository.GetAll();
            return searchKeys;
        }

        public IEnumerable<SearchKey> GetSearchKeys(Expression<Func<SearchKey, bool>> where)
        {
            var searchKeys = searchKeysRepository.GetMany(where);
            return searchKeys;
        }

        public SearchKey GetSearchKey(int id)
        {
            var searchKey = searchKeysRepository.GetById(id);
            return searchKey;
        }

        public void CreateSearchKey(SearchKey searchKey)
        {
            searchKeysRepository.Add(searchKey);
        }

        public void SaveSearchKey()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}

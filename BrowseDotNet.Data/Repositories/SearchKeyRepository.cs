using BrowseDotNet.Data.Infrastructure;
using BrowseDotNet.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Data.Repositories
{
    public class SearchKeyRepository : RepositoryBase<SearchKey>, ISearchKeyRepository
    {
        public SearchKeyRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface ISearchKeyRepository : IRepository<SearchKey>
    {

    }
}

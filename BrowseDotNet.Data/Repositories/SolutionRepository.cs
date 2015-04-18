using BrowseDotNet.Data.Infrastructure;
using BrowseDotNet.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Data.Repositories
{
    public class SolutionRepository : RepositoryBase<Solution>, ISolutionRepository
    {
        public SolutionRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface ISolutionRepository : IRepository<Solution>
    {

    }
}

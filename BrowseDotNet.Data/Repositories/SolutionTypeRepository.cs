using BrowseDotNet.Data.Infrastructure;
using BrowseDotNet.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Data.Repositories
{
    public class SolutionTypeRepository : RepositoryBase<SolutionType>, ISolutionTypeRepository
    {
        public SolutionTypeRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface ISolutionTypeRepository : IRepository<SolutionType>
    {

    }
}

using BrowseDotNet.Data.Infrastructure;
using BrowseDotNet.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Data.Repositories
{
    public class SnippetRepository : RepositoryBase<Snippet>, ISnippetRepository
    {
        public SnippetRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface ISnippetRepository : IRepository<Snippet>
    {

    }
}

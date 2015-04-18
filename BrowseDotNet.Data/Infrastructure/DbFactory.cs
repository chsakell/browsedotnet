using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        BrowseDotNetDbContext dbContext;

        public BrowseDotNetDbContext Init()
        {
            return dbContext ?? (dbContext = new BrowseDotNetDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}

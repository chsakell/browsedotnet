using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseDotNet.Data
{
    public class BrowseDotNetDbInitializer : DropCreateDatabaseIfModelChanges<BrowseDotNetDbContext>
    {
        protected override void Seed(BrowseDotNetDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }
        public void PerformInitialSetup(BrowseDotNetDbContext context)
        {
            // initial configuration will go here
        }

    }
}

using BrowseDotNet.Data;
using BrowseDotNet.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowseDotNet.Web.Infrastructure
{
    public class DotNetUserManager : UserManager<DotNetUser>
    {
        public DotNetUserManager(IUserStore<DotNetUser> store)
            : base(store)
        {
        }

        public static DotNetUserManager Create(IdentityFactoryOptions<DotNetUserManager> options, IOwinContext context)
        {
            BrowseDotNetDbContext db = context.Get<BrowseDotNetDbContext>();
            DotNetUserManager manager = new DotNetUserManager(new UserStore<DotNetUser>(db));
            return manager;
        }
    }
}
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT.Model.CustomSystem
{
    public class AppCustomRoleManager : RoleManager<AppCustomRole, long>
    {
        public AppCustomRoleManager(IRoleStore<AppCustomRole, long> store) : base(store)
        {
        }

        public static AppCustomRoleManager Create(IdentityFactoryOptions<AppCustomRoleManager> options, IOwinContext context)
        {
            var db = context.Get<AppIdentityDbContext>();
            var roleManager = new AppCustomRoleManager(new AppCustomRoleStore(db));
            return roleManager;
        }
    }
}

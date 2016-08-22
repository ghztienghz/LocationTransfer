using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace VT.Model.CustomSystem
{
    public class AppIdentityUser : IdentityUser<long, AppCustomUserLogin, AppCustomUserRole, AppCustomUserClaim>
    {
        // thêm nhiều properties
        public string FullName { get; set; }
        public string Address { get; set; }
        public long IdProvince { get; set; }
        public long IdDistrict { get; set; }
        public long IdWard { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(AppCustomUserManager manager)
        {
            // Note the authenticationType must match the one defined in
            // CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class AppCustomUserLogin : IdentityUserLogin<long> { }
    public class AppCustomUserRole : IdentityUserRole<long> { }
    public class AppCustomUserClaim : IdentityUserClaim<long> { }
    public class AppCustomRole : IdentityRole<long, AppCustomUserRole>
    {
        public AppCustomRole() { }
        public AppCustomRole(string name) { Name = name; }
    }

    public class AppCustomUserStore : UserStore<AppIdentityUser, AppCustomRole, long, AppCustomUserLogin, AppCustomUserRole, AppCustomUserClaim>
    {
        public AppCustomUserStore(AppIdentityDbContext context) : base(context)
        {
        }
    }

    public class AppCustomRoleStore : RoleStore<AppCustomRole, long, AppCustomUserRole>
    {
        public AppCustomRoleStore(AppIdentityDbContext context) : base(context)
        {
        }
    }
}

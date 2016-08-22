using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT.Model.CustomSystem
{
    public class AppIdentityDbContext : IdentityDbContext<AppIdentityUser, AppCustomRole, long, AppCustomUserLogin, AppCustomUserRole, AppCustomUserClaim>
    {
        public AppIdentityDbContext() : base("IdentityConfig")
        {
        }

        static AppIdentityDbContext()
        {
            Database.SetInitializer<AppIdentityDbContext>(new IdentityDbInit());
        }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
    }

    //public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppIdentityDbContext>
    //{
    //    protected override void Seed(AppIdentityDbContext context)
    //    {
    //        Init(context);
    //        base.Seed(context);
    //    }

    //    public void Init(AppIdentityDbContext context)
    //    {
    //        // cấu hình cho tài khoản đầu tiên
    //        AppCustomUserManager uManager = new AppCustomUserManager(new AppCustomUserStore(context));
    //        AppCustomRoleManager rManager = new AppCustomRoleManager(new AppCustomRoleStore(context));
    //        AppIdentityUser user = uManager.FindByName("admin");
    //        if (user == null)
    //        {
    //            user = new AppIdentityUser()
    //            {
    //                UserName = "admin",
    //                PasswordHash = uManager.PasswordHasher.HashPassword("Qazxsw123"),
    //                IdProvince = -1,
    //                IdDistrict = -1,
    //                IdWard = -1
    //            };
    //            IdentityResult rs = uManager.Create(user);
    //            if (rs.Succeeded)
    //            {
    //                AppCustomRole role = new AppCustomRole("superadmin");
    //                rManager.Create(role);
    //                uManager.AddToRole(user.Id, "superadmin");
    //            }

    //        }
    //    }
    //}

    //public class IdentityDbInit : CreateDatabaseIfNotExists<AppIdentityDbContext>
    //{
    //    protected override void Seed(AppIdentityDbContext context)
    //    {
    //        base.Seed(context);
    //    }
    //}

    public class IdentityDbInit : NullDatabaseInitializer<AppIdentityDbContext>
    {
    }
}

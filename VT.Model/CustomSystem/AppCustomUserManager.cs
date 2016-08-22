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
    public class AppCustomUserManager : UserManager<AppIdentityUser, long>
    {
        public AppCustomUserManager(IUserStore<AppIdentityUser, long> store) : base(store)
        {
        }

        public static AppCustomUserManager Create(IdentityFactoryOptions<AppCustomUserManager> options, IOwinContext context)
        {
            AppIdentityDbContext db = context.Get<AppIdentityDbContext>();
            AppCustomUserManager manager = new AppCustomUserManager(new AppCustomUserStore(db));
            manager.PasswordValidator = new PasswordValidator()
            {
                RequireDigit = false,
                RequiredLength = 6,
                RequireLowercase = false,
                RequireUppercase = false
            };
            manager.UserValidator = new UserValidator<AppIdentityUser, long>(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };
            return manager;
        }
    }
}

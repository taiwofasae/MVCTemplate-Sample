using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HelloWorld.Models;
using IdentityManager;
using IdentityManager.AspNetIdentity;
using IdentityManager.Configuration;

namespace HelloWorld.Extensions
{
    public static class IdentityManagerExtensions
    {
        public static void ConfigureSimpleIdentityManagerService(this IdentityManagerServiceFactory factory)
        {
            factory.Register(new Registration<ApplicationDbContext>(resolver => ApplicationDbContext.Create()));
            factory.Register(new Registration<ApplicationUserStore>());
            factory.Register(new Registration<ApplicationRoleStore>());
            factory.Register(new Registration<ApplicationUserManager>());
            factory.Register(new Registration<ApplicationRoleManager>());
            factory.IdentityManagerService = new Registration<IIdentityManagerService, SimpleIdentityManagerService>();
        }
    }
    public class SimpleIdentityManagerService : AspNetIdentityManagerService<ApplicationUser, string, ApplicationRole, string>
    {
        public SimpleIdentityManagerService(ApplicationUserManager userMgr, ApplicationRoleManager roleMgr)
            : base(userMgr, roleMgr)
        {
        }
    }
}
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;
using HelloWorld.Extensions;
using IdentityManager.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartupAttribute(typeof(HelloWorld.Startup))]
namespace HelloWorld
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseOpenIdConnectAuthentication(new Microsoft.Owin.Security.OpenIdConnect.OpenIdConnectAuthenticationOptions
            {
                AuthenticationType = "oidc",
                Authority = "https://bootstrapmvc-idsvr.azurewebsites.net/",
                ClientId = "sample",
                RedirectUri = "http://localhost:38319/admin",//http://sample.test.vggdev.com/
                ResponseType = "id_token",
                UseTokenLifetime = false,
                Scope = "openid profile roles",
                SignInAsAuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            });

            var factory = new IdentityManagerServiceFactory();
            factory.ConfigureSimpleIdentityManagerService();
            var options = new IdentityManagerOptions
            {
                Factory = factory,
                SecurityConfiguration = new HostSecurityConfiguration
                {
                    RequireSsl = false,
                    HostAuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    AdminRoleName = "sample",// == clientid == user's role claim
                    NameClaimType = "name",
                    RoleClaimType = "role",
                }

            };
            app.Map("/admin", _ => { _.UseIdentityManager(options); });
        }
    }
}

using Microsoft.Owin.Security.OAuth;
using School.Domain.Contracts.Services;
using School.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace School.Api.Security
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private const string OAUTH_USERNAME = "mersocarlin";
        private const string OAUTH_PASSWORD = "BolshoiBooze";

        private readonly IStudentService _service;

        public AuthorizationServerProvider(IStudentService service)
        {
            _service = service;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            try
            {
                //You can use your service layer to authenticate your requests
                //This sample is just a brief demonstration 
                
                //var user = _service.Authenticate(context.UserName, context.Password);

                if (context.UserName != OAUTH_USERNAME || context.Password != OAUTH_PASSWORD)
                {
                    context.SetError("invalid_grant", "invalid credentials");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim(ClaimTypes.Name, OAUTH_USERNAME));
                identity.AddClaim(new Claim(ClaimTypes.GivenName, OAUTH_USERNAME));

                GenericPrincipal principal = new GenericPrincipal(identity, null);
                Thread.CurrentPrincipal = principal;

                context.Validated(identity);
            }
            catch (Exception ex)
            {
                context.SetError("invalid_grant", ex.Message);
            }
        }
    }
}
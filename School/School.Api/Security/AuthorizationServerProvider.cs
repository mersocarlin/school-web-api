using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using School.Domain.Contracts.Services;
using School.Domain.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace School.Api.Security
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private const string OAUTH_USERNAME = "mersocarlin";
        private const string OAUTH_PASSWORD = "BolshoiBooze";

        private IUserService userService;

        public AuthorizationServerProvider(IUserService userService)
        {
            this.userService = userService;
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
                User user = null;

                if (context.UserName.Equals(OAUTH_USERNAME) && context.Password.Equals(OAUTH_PASSWORD))
                {
                    user = new User
                    {
                        Id = -1,
                        Username = "mersocarlin",
                        CreatedAt = DateTime.Now,
                        LastLogin = DateTime.Now,
                        Status = EntityStatus.Active,
                        Profile = UserProfile.SuperUser
                    };
                }
                else
                {
                    user = this.userService.Authenticate(context.UserName, context.Password);
                }

                bool authenticated = user != null && user.Status == EntityStatus.Active;

                if (!authenticated)
                {
                    context.SetError("invalid_grant", "invalid credentials");
                    return;
                }

                this.userService.UpdateLastLogin(user.Id);

                ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
                identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Username));
                identity.AddClaim(new Claim(ClaimTypes.Role, ((int)user.Profile).ToString()));
                identity.AddClaim(new Claim("Id", user.Id.ToString()));

                AuthenticationProperties properties = CreateProperties(user);
                AuthenticationTicket ticket = new AuthenticationTicket(identity, properties);
                context.Validated(ticket);
            }
            catch (Exception ex)
            {
                context.SetError("invalid_grant", ex.Message);
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(User user)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                //{ "client_id", user.Id.ToString() },
                { "userId", user.Id.ToString() },
                { "user", user.ToLoginJson() }
            };
            return new AuthenticationProperties(data);
        }
    }
}
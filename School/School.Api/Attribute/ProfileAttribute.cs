using School.Domain.Models;
using System;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace School.Api.Attribute
{
    public class ProfileAttribute : AuthorizeAttribute
    {
        public UserProfile Profile { get; set; }

        public ProfileAttribute(UserProfile Profile)
        {
            this.Profile = Profile;
        }

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            bool matchProfile = false;

            foreach (Claim claim in (HttpContext.Current.User.Identity as System.Security.Claims.ClaimsIdentity).Claims)
            {
                if (claim.Type != ClaimTypes.Role) continue;

                matchProfile = Convert.ToInt32(claim.Value) <= (int)this.Profile;
                break;
            }

            if (matchProfile)
            {
                base.OnAuthorization(actionContext);
            }
            else
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
        }
    }
}
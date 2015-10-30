using System;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace School.Api.Controllers
{
    public class BaseController : ApiController
    {
        public int GetIdentityId()
        {
            int identityId = 0;
            foreach (Claim claim in (HttpContext.Current.User.Identity as System.Security.Claims.ClaimsIdentity).Claims)
            {
                if (!claim.Type.ToString().Equals("Id")) continue;

                identityId = Convert.ToInt32(claim.Value);
                break;
            }
            return identityId;
        }
    }
}

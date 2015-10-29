using School.Api.Attribute;
using School.Domain.Contracts.Services;
using School.Domain.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace School.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/v1/user")]
    public class UserController : BaseController
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [ProfileAttribute(UserProfile.Teacher)]
        [HttpGet]
        [Route("")]
        //[DeflateCompression]
        //[CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)] //Install-Package Strathweb.CacheOutput.WebApi2
        public Task<HttpResponseMessage> GetUsers()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var result = this.userService.GetUsers();
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [ProfileAttribute(UserProfile.SuperUser)]
        [HttpPost]
        [Route("")]
        //[DeflateCompression]
        //[CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)] //Install-Package Strathweb.CacheOutput.WebApi2
        public Task<HttpResponseMessage> PostUser(User user)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                this.userService.SaveUser(user);
                response = Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        protected override void Dispose(bool disposing)
        {
            this.userService.Dispose();
            base.Dispose(disposing);
        }
    }
}

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
    [RoutePrefix("api/v1/course")]
    public class CourseController : BaseController
    {
        private ICourseService _service;

        public CourseController(ICourseService service)
        {
            this._service = service;
        }

        [HttpGet]
        [Route("")]
        //[DeflateCompression]
        //[CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)] //Install-Package Strathweb.CacheOutput.WebApi2
        public Task<HttpResponseMessage> GetCourses()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var result = _service.Get();
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

        [HttpGet]
        [Route("{id}")]
        //[DeflateCompression]
        //[CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)] //Install-Package Strathweb.CacheOutput.WebApi2
        public Task<HttpResponseMessage> GetCourseById(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var result = _service.GetCourseById(id);
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
        public Task<HttpResponseMessage> PostCourse(Course course)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.Save(this.GetIdentityId(), course);
                response = Request.CreateResponse(HttpStatusCode.OK, course);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { course = course, message = ex.Message });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        protected override void Dispose(bool disposing)
        {
            this._service.Dispose();
        }
    }
}

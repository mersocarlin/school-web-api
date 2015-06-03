using School.Domain.Contracts.Services;
using School.Domain.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace School.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/teachers")]
    public class TeacherController : ApiController
    {
        private ITeacherService _service;

        public TeacherController(ITeacherService service)
        {
            this._service = service;
        }

        [HttpGet]
        [Route("")]
        //[DeflateCompression]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)] //Install-Package Strathweb.CacheOutput.WebApi2
        public Task<HttpResponseMessage> Get()
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

        [HttpPost]
        [Route("")]
        public Task<HttpResponseMessage> Post(Person teacher)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.Save(teacher);
                response = Request.CreateResponse(HttpStatusCode.OK, new { name = teacher.FullName, email = teacher.Email });
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
            _service.Dispose();
        }
    }
}

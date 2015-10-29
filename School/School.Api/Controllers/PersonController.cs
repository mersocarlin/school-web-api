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
    [RoutePrefix("api/v1/person")]
    public class PersonController : BaseController
    {
        private IPersonService _service;

        public PersonController(IPersonService service)
        {
            this._service = service;
        }

        [HttpGet]
        [Route("")]
        //[DeflateCompression]
        //[CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)] //Install-Package Strathweb.CacheOutput.WebApi2
        public Task<HttpResponseMessage> Get(string query, int personType, int personStatus, int page, int pageSize = 50)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var result = _service.Get(query, personType, personStatus, page, pageSize);
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
        public Task<HttpResponseMessage> GetPersonById(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var result = _service.GetPersonById(id);
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
        public Task<HttpResponseMessage> Post(Person person)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.SavePerson(this.GetIdentityId(), person);
                response = Request.CreateResponse(HttpStatusCode.OK, person);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { person = person.Id, message = ex.Message });
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

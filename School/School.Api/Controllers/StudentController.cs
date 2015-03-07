﻿using School.Domain.Contracts;
using School.Domain.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace School.Api.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentController : ApiController
    {
        private IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            this._studentRepository = studentRepository;
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
                var result = _studentRepository.Get();
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
        public Task<HttpResponseMessage> Post(Person student)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _studentRepository.Create(student);
                response = Request.CreateResponse(HttpStatusCode.OK, new { name = student.FullName, email = student.Email });
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
            _studentRepository.Dispose();
        }
    }
}
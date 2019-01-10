using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.DataProvider;
using WebAPI.DataProvider.EF;
using WebAPI.DataProvider.Models;

namespace WebAPI.Controllers
{
    public class CapabilityController : ApiController
    {
        private CapabilityDB context = new CapabilityDB();
        // GET: api/Layer
        [ResponseType(typeof(IEnumerable<SYS_Capability>))]
        public HttpResponseMessage Get()
        {
            try
            {
                var result = context.GetAll().ToList();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }

        // GET: api/Layer/5
        [ResponseType(typeof(SYS_Capability))]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                var result = context.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, result != null ? result:null);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        // POST: api/Layer
        [ResponseType(typeof(SYS_Capability))]
        public HttpResponseMessage Post([FromBody]SYS_Capability value)
        {
            try
            {
                var result = context.Create(value);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        // PUT: api/Layer/5
        [ResponseType(typeof(SYS_Capability))]
        public HttpResponseMessage Put(string id,[FromBody]SYS_Capability value)
        {
            try
            {
                var result = context.Update(id,value);
                return Request.CreateResponse(HttpStatusCode.OK, result ? value : null);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        // DELETE: api/Layer/5
        [ResponseType(typeof(bool))]
        public HttpResponseMessage Delete(string id)
        {
            try
            {
                var result = context.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}

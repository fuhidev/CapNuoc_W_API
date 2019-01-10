using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.DataProvider.EF;
using WebAPI.DataProvider.SystemManagement;

namespace WebAPI.Controllers
{
    [RoutePrefix("versioning")]
    [Authorize]
    public class VersioningController : ApiController
    {
        private VersioningDB context = new VersioningDB();

        [AllowAnonymous]
        [Route("{applicationId}")]
        [ResponseType(typeof(SYS_Version))]
        public HttpResponseMessage Get(string applicationId,string version)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, context.CheckUpdate(applicationId,version));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [ResponseType(typeof(IEnumerable<SYS_Version>))]
        public HttpResponseMessage Get()
        {
            try
            {
                var results = context.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        // GET: api/Account/5
        [ResponseType(typeof(SYS_Version))]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                var result = context.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        // POST: api/Layer
        [ResponseType(typeof(SYS_Version))]
        public HttpResponseMessage Post([FromBody]SYS_Version value)
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
        [ResponseType(typeof(SYS_Version))]
        public HttpResponseMessage Put(string id, [FromBody]SYS_Version value)
        {
            try
            {
                var result = context.Update(id, value);
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

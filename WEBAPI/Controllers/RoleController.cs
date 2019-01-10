using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.DataProvider;
using WebAPI.DataProvider.EF;

namespace WebAPI.Controllers
{
    [Authorize]
    public class RoleController : ApiController
    {
        private RoleDB context = new RoleDB();
        // GET: api/Account

        [ResponseType(typeof(IEnumerable<SYS_Role>))]
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
        [ResponseType(typeof(SYS_Role))]
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
        [ResponseType(typeof(SYS_Role))]
        public HttpResponseMessage Post([FromBody]SYS_Role value)
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
        [ResponseType(typeof(SYS_Role))]
        public HttpResponseMessage Put(string id, [FromBody]SYS_Role value)
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

    [Authorize]
    public class GroupRoleController : ApiController
    {
        private GroupRoleDB context = new GroupRoleDB();
        // GET: api/Account

        [ResponseType(typeof(IEnumerable<SYS_GroupRole>))]
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
        [ResponseType(typeof(SYS_GroupRole))]
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
        [ResponseType(typeof(SYS_GroupRole))]
        public HttpResponseMessage Post([FromBody]SYS_GroupRole value)
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
        [ResponseType(typeof(SYS_GroupRole))]
        public HttpResponseMessage Put(string id, [FromBody]SYS_GroupRole value)
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

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
    public class LayerController : ApiController
    {
        private LayerDB context = new LayerDB();
        // GET: api/Layer
        [ResponseType(typeof(IEnumerable<SYS_Layer>))]
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
        [ResponseType(typeof(SYS_Layer))]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                var result = context.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, result != null ? new SYS_Layer
                {
                    GroupID = result.GroupID,
                    ID = result.ID,
                    NumericalOder = result.NumericalOder,
                    Title = result.Title,
                    Url = result.Url
                }:null);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        // POST: api/Layer
        [ResponseType(typeof(SYS_Layer))]
        public HttpResponseMessage Post([FromBody]SYS_Layer value)
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
        [ResponseType(typeof(SYS_Layer))]
        public HttpResponseMessage Put(string id,[FromBody]SYS_Layer value)
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

    public class GroupLayerController : ApiController
    {
        private GroupLayerDB context = new GroupLayerDB();
        // GET: api/Layer
        [ResponseType(typeof(IEnumerable<SYS_GroupLayer>))]
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
        [ResponseType(typeof(SYS_GroupLayer))]
        public HttpResponseMessage Get(string id)
        {
            try
            {
                var result = context.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, result != null ? result: null);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        // POST: api/Layer
        [ResponseType(typeof(SYS_GroupLayer))]
        public HttpResponseMessage Post([FromBody]SYS_GroupLayer value)
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
        [ResponseType(typeof(SYS_GroupLayer))]
        public HttpResponseMessage Put(string id, [FromBody]SYS_GroupLayer value)
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

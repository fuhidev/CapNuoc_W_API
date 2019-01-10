using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.DataProvider.EF;
using WebAPI.DataProvider.GIS;
using WebAPI.DataProvider.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("HanhChinh")]
    public class HanhChinhController : ApiController
    {
        private HanhChinhDB context = new HanhChinhDB();

        [Route("GetByPoint")]
        [ResponseType(typeof(HANHCHINH))]
        [HttpGet]
        public HttpResponseMessage GetByPoint([FromBody]Point point)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, this.context.GetByPoint(point));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.InnerException);
            }
        }
    }
}

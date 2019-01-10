using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.DataProvider;
using WebAPI.DataProvider.Models;

namespace WebAPI.Controllers
{

    [Authorize]
    [RoutePrefix("api/layerinfo")]
    public class LayerInfoController: ApiController
    {
        private AccountDB context = new AccountDB();
        public HttpResponseMessage Get()
        {
            try
            {
                var user = User.Identity;
                var result = context.LayerInfos(new DataProvider.EF.SYS_Account
                {
                    Username = user.Name
                });
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}

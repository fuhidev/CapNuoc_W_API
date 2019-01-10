using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.DataProvider;
using WebAPI.DataProvider.EF;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private AccountDB context = new AccountDB();
        // GET: api/Account

        [ResponseType(typeof(IEnumerable<SYS_Account>))]
        public HttpResponseMessage Get()
        {
            try
            {
                var results = context.GetAll()
                    .Select(s => // xóa pass
                {
                    s.Password = null;
                    return s;
                });
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        // GET: api/Account/5
        [ResponseType(typeof(Profile))]
        [Route("Profile")]
        [HttpGet]
        public HttpResponseMessage Profile()
        {
            try
            {
                var user = User.Identity.Name;
                if (user != null)
                {
                    var result = context.Get(user);
                    var capabilitites = new List<string>();
                    if (result.SYS_Capability_Account != null && result.SYS_Capability_Account.Count > 0)
                    {
                        foreach (var cap in result.SYS_Capability_Account)
                        {
                            capabilitites.Add(cap.Capability);
                        }
                    }
                    var profile = new Profile
                    {
                        DisplayName = result.DisplayName,
                        Username = result.Username,
                        Role = result.Role,
                        Capabilities = capabilitites
                    };
                    // xóa pass
                    result.Password = null;
                    return Request.CreateResponse(HttpStatusCode.OK, profile);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Không tìm thấy định danh");
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        // GET: api/Account/5
        [ResponseType(typeof(Profile))]
        [Route("IsAccess/{idApp}")]
        [HttpGet]
        public HttpResponseMessage IsAccess(string idApp)
        {
            if (String.IsNullOrEmpty(idApp)) return Request.CreateResponse(HttpStatusCode.OK, false);
            try
            {
                var user = User.Identity.Name;
                if (user != null)
                {
                    var result = context.Get(user);
                    var capabilitites = new List<string>();
                    if (result.SYS_Capability_Account != null && result.SYS_Capability_Account.Count > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, result.SYS_Capability_Account.Any(a => a.Capability.Equals(idApp)));
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, false);
                    }

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Không tìm thấy định danh");
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        // POST: api/Layer
        [ResponseType(typeof(SYS_Account))]
        public HttpResponseMessage Post([FromBody]SYS_Account value)
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
        [ResponseType(typeof(SYS_Account))]
        [Route("UpdateProfile/{id}")]
        [HttpPut]
        public HttpResponseMessage UpdateProfile(string id, [FromBody]SYS_Account value)
        {
            try
            {
                // lấy account mặc định
                var account = context.Get(id);
                if (value.Password != null)
                {
                    account.Password = value.Password;
                }
                account.DisplayName = value.DisplayName;
                var result = context.Update(id, account);
                return Request.CreateResponse(HttpStatusCode.OK, result ? value : null);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        // PUT: api/Layer/5
        [ResponseType(typeof(SYS_Account))]
        public HttpResponseMessage Put(string id, [FromBody]SYS_Account value)
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

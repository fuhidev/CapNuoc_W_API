using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.DataProvider;
using WebAPI.DataProvider.EF;
using WebAPI.Models;


namespace WebAPI.Controllers
{
    [RoutePrefix("Login")]
    public class LoginController : ApiController
    {
        private AccountDB provider = new AccountDB();
        [HttpPost]
        public HttpResponseMessage Authenticate([FromBody] LoginRequest login)
        {
            // if credentials are valid
            if (this.Login(login.Username, login.Password) != null)
            {
                var tokenValidator = new TokenValidationHandler();
                string token = tokenValidator.CreateToken(login.Username);
                //return the token
                return Request.CreateResponse(HttpStatusCode.OK, token);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Tài khoản hoặc mật khẩu không đúng");
            }
        }


        [HttpPost]
        [Route("WithUser")]
        public HttpResponseMessage WithUser([FromBody] LoginRequest login)
        {
            var baseAccount = this.Login(login.Username, login.Password);

            // if credentials are valid
            if (baseAccount != null)
            {
                var tokenValidator = new TokenValidationHandler();
                string token = tokenValidator.CreateToken(login.Username);
                var capabilities = new List<string>();
                if (baseAccount.SYS_Capability_Account != null && baseAccount.SYS_Capability_Account.Count > 0)
                {
                    foreach (var cap in baseAccount.SYS_Capability_Account)
                    {
                        capabilities.Add(cap.Capability);
                    }
                }
                //return the token
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    username = baseAccount.Username,
                    displayname = baseAccount.DisplayName,
                    token = token,
                    capabilities = capabilities
                });
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Tài khoản hoặc mật khẩu không đúng");
            }
        }

        private SYS_Account Login(string username, string password)
        {
            return provider.IsValid(new SYS_Account { Username = username, Password = password });
        }
    }
}

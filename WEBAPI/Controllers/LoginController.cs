using System;
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
    public class LoginController : ApiController
    {
        private AccountDB provider = new AccountDB();

        public HttpResponseMessage Get(string Username, string Password)
        {
            return this.Authenticate(new LoginRequest
            {
                Username = Username,
                Password = Password
            });
        }

        [HttpPost]
        public HttpResponseMessage Authenticate([FromBody] LoginRequest login)
        {
            var loginResponse = new LoginResponse { };
            SYS_Account loginRequest = new SYS_Account
            {
                Username = login.Username.ToLower(),
                Password = login.Password
            };

            bool isUsernamePasswordValid = false;

            if (login != null)
            {
                try
                {
                    isUsernamePasswordValid = provider.IsValid(loginRequest) != null;
                }
                catch (Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
                }
            }

            // if credentials are valid
            if (isUsernamePasswordValid)
            {
                var tokenHandler = new TokenValidationHandler();
                string token = tokenHandler.CreateToken(loginRequest.Username);
                //return the token
                return Request.CreateResponse(HttpStatusCode.OK, token);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Tài khoản hoặc mật khẩu không đúng");
            }
        }

        [HttpGet]
        [Route("api/Login/profile")]
        [ResponseType(typeof(SYS_Account))]
        public HttpResponseMessage Profile()
        {
            try
            {
                var user = User.Identity;
                var result = this.provider.Get(user.Name);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}

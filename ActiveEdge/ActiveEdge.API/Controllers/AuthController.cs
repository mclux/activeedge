using ActiveEdge.API.Providers;
using ActiveEdge.Core;
using ActiveEdge.Infrastructure.DTO;
using ActiveEdge.Infrastructure.Services;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ActiveEdge.API.Controllers
{
    [AllowAnonymous]
    public class AuthController : ApiController
    {
        
        public AuthController()
        {
        }

        /// <summary>
        /// Authentication endpoint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(string))]
        public IHttpActionResult Login(LoginRequestDto request)
        {
            try
            {
                if(request.Username!="test" && request.Password!="password")
                {
                    return BadRequest("Invalid credentials.");
                }

                var userIdentity = new UserIdentityData
                {
                    FullName = "Jack Thor",
                    UserId = 1,
                    Username = request.Username,
                    UserRole = "TESTER"
                };
                var jwt = JWTokenProvider.CreateToken(userIdentity, "TESTER");

                var result = new LoginResponseDto { 
                    Username=request.Username,
                    Token=jwt
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ActiveEdgeConstants.GENERIC_ERROR_MSG);
            }
        }
    }
}

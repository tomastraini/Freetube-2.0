using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using REST.Authentication;
using REST.Contexts;
using REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace REST.Controladores
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IJWTAuth jwtAuth;
        public readonly ContextoGeneral ctx;

        public AuthenticationController(IJWTAuth jwtAuth, ContextoGeneral ctx)
        {
            this.jwtAuth = jwtAuth;
            this.ctx = ctx;
        }
        [AllowAnonymous]
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] Member userCredential)
        {
            var users = (from us in ctx.users select us).ToList();

            var token = jwtAuth.Authentication(userCredential.Name, userCredential.Password, users);
            
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

    }
}

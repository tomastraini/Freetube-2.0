using Microsoft.AspNetCore.Mvc;
using REST.DTOs;
using REST.Models;
using REST.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace REST.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IUsersService service;
        public UsersController(IUsersService service)
        {
            this.service = service;
        }
        [HttpGet]
        public ActionResult GetUsers()
        {
            return Ok(service.GetUsers());
        }
        [HttpPost]
        public ActionResult Register([FromBody] UsersDTO users)
        {
            return Ok(service.Register(users));
        }
        [HttpPatch]
        public ActionResult ChangePassword(int id_user, string pass)
        {
            return Ok(service.ChangePassword(id_user, pass));
        }
    }
}

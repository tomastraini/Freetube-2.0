using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using REST.DTOs;
using REST.Models;
using REST.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace REST.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private string filepath;
        private string _targetFilePath;
        public readonly IUsersService service;
        public UsersController(IConfiguration config, IUsersService service)
        {
            this.service = service;
            filepath = config.GetSection("StoredUsersImage").Value.ToString();


            _targetFilePath = CheckDirectory(filepath);

            string CheckDirectory(string directory)
            {

                if (System.IO.Directory.Exists(directory) == true)
                {
                    return directory;
                }
                else
                {
                    if (directory.Contains(":"))
                    {
                        string[] totalfilepathmain = { directory, filepath };
                        var _targetFilePath = Path.Combine(totalfilepathmain);
                        Directory.CreateDirectory(_targetFilePath);
                        return _targetFilePath;
                    }
                    else
                    {
                        directory = Directory.GetCurrentDirectory();
                        string[] totalfilepathmain = { directory, filepath };
                        var _targetFilePath = Path.Combine(totalfilepathmain);
                        Directory.CreateDirectory(_targetFilePath);
                        return _targetFilePath;
                    }
                }
            }
        }
      
        [HttpGet]
        public ActionResult GetUsers()
        {
            return Ok(service.GetUsers());
        }

        [HttpGet("{id}")]
        public ActionResult GetUsers(string id)
        {
            return Ok(service.GetUserById(id));
        }
        [HttpPost]
        public ActionResult Register(IFormFile files, string username, string password, string correo,
            string nombreyapellido, string telefono)
        {
            service.Register(username, password, correo, nombreyapellido, telefono, files, _targetFilePath);
            return Ok();
        }
        [HttpPatch]
        public ActionResult ChangePassword(int id_user, string oldpass, string newpass)
        {
            return Ok(service.ChangePassword(id_user, oldpass, newpass));
        }

        [HttpPatch("Image")]
        public ActionResult ChangeImage(int id_user, IFormFile files)
        {
            service.ChangeImage(id_user, files, _targetFilePath);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] UsersDTO users)
        {
            if(users.usern == null || users.usern == "" ||
            users.passwordu == null || users.passwordu == "") { return null; }
            return Ok(service.Login(users));
        }
    }
}

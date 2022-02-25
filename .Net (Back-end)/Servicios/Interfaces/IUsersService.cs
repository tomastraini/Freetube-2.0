using Microsoft.AspNetCore.Http;
using REST.DTOs;
using REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Servicios.Interfaces
{
    public interface IUsersService
    {
        public List<users> GetUsers(); 
        public void Register(string username, string password, IFormFile file, string filepath);
        public users ChangePassword(int id, string pass);
    }
}

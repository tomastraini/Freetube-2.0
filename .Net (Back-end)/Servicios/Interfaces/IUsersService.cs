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
        public List<UsersDTO> GetUsers(); 
        public void Register(string username, string password, string correo,
            string nombreyapellido, string telefono, IFormFile file, string filepath);
        public users ChangePassword(int id, string pass);
        public void ChangeImage(int id_user, IFormFile files, string filePath);
    }
}

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
        public UsersDTO GetUserById(string id);
        public void Register(string username, string password, string correo,
            string nombreyapellido, string telefono, IFormFile file, string filepath);
        public users ChangePassword(string id, string oldpass, string newpass);
        public void ChangeImage(int id_user, IFormFile files, string filePath);
        public users Login(UsersDTO users);
        public byte[] GetImageById(UsersDTO id_img);
    }
}

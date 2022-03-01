using REST.DTOs;
using REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Repositories.Interfaces
{
    public interface IUsersRepo
    {
        public List<UsersDTO> GetUsers();
        public users ChangePassword(int id, string pass);
        public  users Register(string username, string password
            , string correo,
            string nombreyapellido, string telefono, string finalfpath);
        public string ChangeImage(int id_user, string filePath);
    }
}

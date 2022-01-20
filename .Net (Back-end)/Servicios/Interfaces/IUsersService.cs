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
        public users Register(UsersDTO users);
        public users ChangePassword(int id, string pass);
    }
}

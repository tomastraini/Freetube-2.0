using REST.DTOs;
using REST.Models;
using REST.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Servicios.Interfaces
{
    public class UsersService : IUsersService
    {
        public readonly IUsersRepo repo;
        public UsersService(IUsersRepo repo)
        {
            this.repo = repo;
        }

        public users ChangePassword(int id, string pass)
        {
            return repo.ChangePassword(id, pass);
        }

        public List<users> GetUsers()
        {
            return repo.GetUsers();
        }

        public users Register(UsersDTO users)
        {
            return repo.Register(users);
        }
    }
}

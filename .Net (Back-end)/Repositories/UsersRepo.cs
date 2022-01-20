using REST.Contexts;
using REST.DTOs;
using REST.Models;
using REST.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Repositories
{
    public class UsersRepo : IUsersRepo
    {
        public readonly ContextoGeneral ctx; 
        public UsersRepo(ContextoGeneral ctx)
        {
            this.ctx = ctx;
        }

        public users ChangePassword(int id, string pass)
        {
            var query = (from us in ctx.users
                         where us.id_user == id
                         select us).FirstOrDefault();
            if(query == null) { return null; };
            query.passwordu = pass;
            ctx.SaveChanges();
            return query;
        }

        public List<users> GetUsers()
        {
            var response = (from us in ctx.users
                            select us).ToList();
            return response;
        }

        public users Register(UsersDTO users)
        {
            var query = (from us in ctx.users
                         where us.usern == users.usern
                         select us).FirstOrDefault();
            if(query != null) { return null; }

            var response = new users();
            response.id_user = 0;
            response.passwordu = users.passwordu;
            response.usern = users.usern;

            ctx.users.Add(response);
            ctx.SaveChanges();
            return response;
        }
    }
}

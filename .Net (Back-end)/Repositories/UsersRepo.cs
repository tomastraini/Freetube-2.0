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

        public string ChangeImage(int id_user, string filePath)
        {
            var response = (from users in ctx.users
                            where users.id_user == id_user
                            select users).FirstOrDefault();
            if(response != null)
            {
                if(response.imagepath != null && response.imagepath != "" && !response.imagepath.Contains("default.png"))
                {
                    return response.imagepath;
                }else
                {
                    response.imagepath = filePath;
                    ctx.SaveChanges();
                    return filePath;
                }
            }
            return "error";
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

        public List<UsersDTO> GetUsers()
        {
            var response = (from us in ctx.users
                            join rol in ctx.roles on us.id_rol equals rol.id_rol
                            select new UsersDTO()
                            { 
                                passwordu = us.passwordu,
                                usern = us.usern,
                                rol = rol.nombre_rol
                            }).ToList();
            return response;
        }

        public users Register(string username, string password
            , string correo,
            string nombreyapellido, string telefono, string finalfpath)
        {
            var query = (from us in ctx.users
                         where us.usern == username
                         select us).FirstOrDefault();
            if(query != null) { return null; }

            var response = new users();
            response.id_user = 0;
            response.usern = username;
            response.passwordu = password;
            response.imagepath = finalfpath;
            response.id_rol = 1;
            response.correo = correo;
            response.telefono = telefono;
            response.nombreyapellido = nombreyapellido;

            ctx.users.Add(response);
            ctx.SaveChanges();
            return response;
        }
    }
}

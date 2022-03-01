using REST.Contexts;
using REST.DTOs;
using REST.Models;
using REST.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
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

        public users ChangePassword(int id, string oldpass, string newpass)
        {
            var passwordToVerify = "";
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(oldpass));

                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                passwordToVerify = stringbuilder.ToString();
            }


            var query = (from us in ctx.users
                         where us.id_user == id
                         && us.passwordu == passwordToVerify
                         select us).FirstOrDefault();
            if(query == null) { return null; };

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(newpass));

                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                query.passwordu = stringbuilder.ToString();
                ctx.SaveChanges();
                return query;
            }

           
        }

        public UsersDTO GetUserById(string id)
        {
            return (from users in ctx.users
                    join rol in ctx.roles on users.id_rol equals rol.id_rol
                    where users.usern == id
                    select new UsersDTO()
                    {
                        usern = users.usern,
                        passwordu = users.passwordu,
                        rol = rol.nombre_rol
                    }).FirstOrDefault();
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

        public users Login(UsersDTO users)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(users.passwordu));

                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                users.passwordu = stringbuilder.ToString();
            }
            return (from us in ctx.users
                    where us.usern == users.usern
                    && us.passwordu == users.passwordu
                    select us).FirstOrDefault();
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

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                response.passwordu = stringbuilder.ToString();
            }
             


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

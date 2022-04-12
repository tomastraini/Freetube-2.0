using REST.Contexts;
using REST.DTOs;
using REST.Models;
using REST.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
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
        public readonly string publickey = "161481A$";
        public readonly string secretkey = "Es13dla1";
        public UsersRepo(ContextoGeneral ctx)
        {
            this.ctx = ctx;
        }
        //************************** Encryption and decryption **************************//
        public string Encrypt(string textToEncrypt)
        {
            try
            {
                string ToReturn = "";
                byte[] secretkeyByte = { };
                secretkeyByte = System.Text.Encoding.UTF8.GetBytes(this.secretkey);
                byte[] publickeybyte = { };
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(this.publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    ToReturn = Convert.ToBase64String(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public string Decrypt(string textToDecrypt)
        {
            try
            {
                string ToReturn = "";
                byte[] privatekeyByte = { };
                privatekeyByte = System.Text.Encoding.UTF8.GetBytes(this.secretkey);
                byte[] publickeybyte = { };
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(this.publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
                inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    ToReturn = encoding.GetString(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ae)
            {
                throw new Exception(ae.Message, ae.InnerException);
            }
        }
        //************************** End of Encryption and decryption **************************//

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

        public users ChangePassword(string id, string oldpass, string newpass)
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
                         where us.usern == id
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
            var response = (from users in ctx.users
                    join rol in ctx.roles on users.id_rol equals rol.id_rol
                    where users.usern == id
                    select new UsersDTO()
                    {
                        usern = users.usern,
                        passwordu = users.passwordu,
                        rol = rol.nombre_rol
                    }).FirstOrDefault();
            response.usern = this.Encrypt(response.usern);

            return response;
        }

        public users GetUserByIdWithoutDTO(string id, bool decrypt)
        {
            id = decrypt ? this.Decrypt(id) : id;
            bool isNumber = int.TryParse(id, out int num);

            if(isNumber)
            {
                return (from users in ctx.users
                        where users.id_user == num
                        select users).FirstOrDefault();
            }
            else
            {
                return (from users in ctx.users
                        where users.usern == id
                        select users).FirstOrDefault();
            }

        }

        public List<UsersDTO> GetUsers()
        {
            var response = (from us in ctx.users
                            join rol in ctx.roles on us.id_rol equals rol.id_rol
                            select new UsersDTO()
                            { 
                                usern = us.usern,
                                passwordu = us.passwordu,
                                rol = rol.nombre_rol
                            }).ToList();
            response.ForEach(e => 
            {
                e.usern = this.Encrypt(e.usern);
            });
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


            var response = (from us in ctx.users
                    where us.usern == users.usern
                    && us.passwordu == users.passwordu
                    select us).FirstOrDefault();

            if(response.usern != null)
            {
                response.usern = this.Encrypt(response.usern);
            }

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

            var response = new users
            {
                id_user = 0,
                usern = username
            };

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

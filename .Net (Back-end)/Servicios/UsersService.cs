using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using REST.DTOs;
using REST.Models;
using REST.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REST.Servicios.Interfaces
{
    public class UsersService : IUsersService
    {
        public readonly IUsersRepo repo;
        private string filepath;
        public UsersService(IConfiguration config, IUsersRepo repo)
        {
            filepath = config.GetSection("StoredUsersImage").Value.ToString();
            this.repo = repo;
        }
        // Funciones aparte
        string CheckDirectory(string directory)
        {
            if (Directory.Exists(directory) == true)
            {
                return directory;
            }
            else
            {
                if (directory.Contains(":"))
                {
                    string[] totalfilepathmain = { directory, filepath };
                    var _targetFilePath = Path.Combine(totalfilepathmain);
                    Directory.CreateDirectory(_targetFilePath);
                    return _targetFilePath;
                }
                else
                {
                    directory = Directory.GetCurrentDirectory();
                    string[] totalfilepathmain = { directory, filepath };
                    var _targetFilePath = Path.Combine(totalfilepathmain);
                    Directory.CreateDirectory(_targetFilePath);
                    return _targetFilePath;
                }
            }
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        public users ChangePassword(string id, string oldpass, string newpass)
        {
            return repo.ChangePassword(id, oldpass, newpass);
        }

        public List<UsersDTO> GetUsers()
        {
            return repo.GetUsers();
        }

        public async void Register(string username, string password, string correo,
            string nombreyapellido, string telefono, IFormFile files, string filePath)
        {
            if(files != null)
            { 
                var ext = Path.GetExtension(files.FileName);
                filePath = Path.Combine(filePath,
                Path.GetRandomFileName().Replace(".", string.Empty) + ext);
                var finalfilepath = this.CheckDirectory(filePath);
                finalfilepath += "\\" + Path.GetFileName(filePath);
                repo.Register(username, password, correo, nombreyapellido, telefono, finalfilepath);
                if (files.Length > 0)
                {
                    using var stream = File.Create(filePath);
                    try
                    {
                        await files.CopyToAsync(stream);

                    }
                    catch (Exception e)
                    {
                        e.InnerException.ToString();
                    }
                }
            }
            else
            {
                filePath = Path.Combine(filePath,
                "default.png");
                var finalfilepath = this.CheckDirectory(filePath);
                finalfilepath += "\\" + Path.GetFileName(filePath);

                repo.Register(username, password, correo, nombreyapellido, telefono, finalfilepath);
            }

        }

        public void ChangeImage(int id_user, IFormFile files, string filePath)
        {
            if(files == null) { return; }
            var ext = Path.GetExtension(files.FileName);
            filePath = Path.Combine(filePath,
            Path.GetRandomFileName().Replace(".", string.Empty) + ext);
            var finalfilepath = this.CheckDirectory(filePath);
            finalfilepath += "\\" + Path.GetFileName(filePath);
            var changeFile = repo.ChangeImage(id_user, finalfilepath);
            if(changeFile != "error")
            {
                if (File.Exists(changeFile))
                {
                    File.Delete(changeFile);
                }
                using var stream = File.Create(changeFile);
                try
                {
                    files.CopyTo(stream);
                }
                catch (Exception e)
                {
                    e.InnerException.ToString();
                }
            }

        }

        public UsersDTO GetUserById(string id)
        {
            return repo.GetUserById(id);
        }

        public users Login(UsersDTO users)
        {
            return repo.Login(users);
        }

        public byte[] GetImageById(string id_img, bool decrypt)
        {
            var imageToWatch = repo.GetUserByIdWithoutDTO(id_img, decrypt);

            if (imageToWatch != null && File.Exists(imageToWatch.imagepath))
            {
                var _fileContent = new FileContentResult(System.IO.File.ReadAllBytes(
                                                   imageToWatch.imagepath),
                                                   "application/octet-stream");
                byte[] byteArray = _fileContent.FileContents;
                return byteArray;
            }
            return Encoding.ASCII.GetBytes("NotFound!!");
        }
    }
}

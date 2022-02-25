using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using REST.DTOs;
using REST.Models;
using REST.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            if (System.IO.Directory.Exists(directory) == true)
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
        public users ChangePassword(int id, string pass)
        {
            return repo.ChangePassword(id, pass);
        }

        public List<users> GetUsers()
        {
            return repo.GetUsers();
        }

        public async void Register(string username, string password, IFormFile files, string filePath)
        {
            var ext = Path.GetExtension(files.FileName);
            filePath = Path.Combine(filePath,
            Path.GetRandomFileName().Replace(".", string.Empty) + ext);
            var finalfilepath = this.CheckDirectory(filePath);
            finalfilepath += "\\" + Path.GetFileName(filePath);
            repo.Register(username, password, finalfilepath);
            if (files.Length > 0)
            {
                using (var stream = System.IO.File.Create(filePath))
                {
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
        }
    }
}

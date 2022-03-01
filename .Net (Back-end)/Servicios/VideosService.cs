using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using REST.Contexts;
using REST.Controladores;
using REST.DTOs;
using REST.Models;
using REST.Repositories.Interfaces;
using REST.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REST.Servicios
{
    public class VideosService : IVideosService
    {
        public readonly IVideosRepo repo;
        private string filepath;
        public VideosService(IConfiguration config, IVideosRepo repo)
        {
            this.repo = repo;
            filepath = config.GetSection("StoredFilesPath").Value.ToString();
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

        public List<videosDTO> ListVideos()
        {
            return repo.ListVideos();
        }

        public videos OnPostUploadAsync(IFormFile files, string title, string description, int id_user, string filePath)
        {
            var ext = Path.GetExtension(files.FileName);
            filePath = Path.Combine(filePath,
            Path.GetRandomFileName().Replace(".", string.Empty) + ext);
            var finalfilepath = this.CheckDirectory(filePath);
            finalfilepath += "\\" + Path.GetFileName(filePath);

            if (files.Length > 0)
            {
                using (var stream = System.IO.File.Create(filePath))
                {
                    try
                    {
                        files.CopyTo(stream);
                        return repo.UploadBD(title, description, finalfilepath, id_user);
                    }
                    catch (Exception e) {
                        e.InnerException.ToString();
                        return null;
                    }
                }
            }
            return null;
        }

        public videos ModifyVideo(videos videos)
        {
            var response = videos;
            response = repo.ModifyVideo(response);

            return videos;
        }

        public string DestroyVideo(int id)
        {
            var response = repo.DestroyVideo(id);
            return response;
        }

        public byte[] watchVideo(int id)
        {
            var vidtoWatch = repo.watchVideo(id);
            if(vidtoWatch != null)
            {
                var _fileContent = new FileContentResult(System.IO.File.ReadAllBytes(
                                                   vidtoWatch.paths),
                                                   "application/octet-stream");
                byte[] byteArray = _fileContent.FileContents;
                return byteArray;
            }
            return Encoding.ASCII.GetBytes("NotFound!!");
        }

        public videos OneVideo(int id)
        {
            return repo.OneVideo(id);
        }
    }
}

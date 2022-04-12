using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using REST.Authentication;
using REST.Contexts;
using REST.Models;
using REST.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private string filepath;
        private string _targetFilePath;

        public readonly IVideosService videosService;
        public readonly ContextoGeneral contexto;

        public VideosController(IConfiguration config, IVideosService videosService, 
            ContextoGeneral contexto)
        {
            this.contexto = contexto;
            this.videosService = videosService;


            filepath = config.GetSection("StoredFilesPath").Value.ToString();


            _targetFilePath = CheckDirectory(filepath);

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
        }

        [HttpGet]
        public ActionResult ListVideos()
        {
            return Ok(videosService.ListVideos());
        }

        [HttpGet("{id}")]
        public ActionResult OneVideo(int id)
        {
            return Ok(videosService.OneVideo(id));
        }

        [HttpPost]
        public ActionResult OnPostUpload(IFormFile files, string title, string description, int id_user)
        {
            
            return Ok(videosService.OnPostUploadAsync(files, title, description, id_user, _targetFilePath));
        }

        [HttpPut]
        public ActionResult ModifyVideo([FromBody] videos videos)
        {
            return Ok(videosService.ModifyVideo(videos));
        }
        [HttpDelete]
        public ActionResult DestroyVideo(int id)
        {
            return Ok(videosService.DestroyVideo(id));
        }
        [HttpGet("Watch")]
        public IActionResult watchVideo(int id)
        {
            var video = videosService.watchVideo(id);
            return File(video, "application/octet-stream");
        }

        [HttpPost("getIfLiked")]
        public ActionResult getIfLiked(likes like)
        {
            
            return Ok(videosService.getIfLiked(like));
        }

        [HttpPost("like")]
        public ActionResult likeVideo(likes like)
        {
            videosService.likeVideo(like);
            return Ok();
        }
        [HttpDelete("like")]
        public ActionResult deleteLike(int id_video, int id_user)
        {
            var like = new likes() { id_video = id_video, id_user = id_user };
            videosService.deleteLike(like);
            return Ok();
        }
    }
}

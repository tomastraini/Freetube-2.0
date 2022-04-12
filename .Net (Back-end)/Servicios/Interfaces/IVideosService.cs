using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REST.Contexts;
using REST.DTOs;
using REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Servicios.Interfaces
{
    public interface IVideosService
    {
        List<videosDTO> ListVideos();
        videos OnPostUploadAsync(IFormFile files, string title, string description, int id_user, string path);
        videos ModifyVideo(videos videos);
        string DestroyVideo(int id);
        byte[] watchVideo(int id);
        videosDTO OneVideo(int id);
        public void likeVideo(likes like);
        public int getIfLiked(likes like);
        public void deleteLike(likes like);

    }
}

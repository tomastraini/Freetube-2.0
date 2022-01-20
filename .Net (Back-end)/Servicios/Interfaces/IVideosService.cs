using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REST.Contexts;
using REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Servicios.Interfaces
{
    public interface IVideosService
    {
        List<videos> ListVideos();

        Task<videos> OnPostUploadAsync(IFormFile files, string title, string description, string path);

        videos ModifyVideo(videos videos);

        string DestroyVideo(int id);
    }
}

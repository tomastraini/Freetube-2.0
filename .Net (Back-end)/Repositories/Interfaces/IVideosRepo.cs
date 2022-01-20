using REST.Contexts;
using REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Repositories.Interfaces
{
    public interface IVideosRepo
    {
        videos OnPostUploadAsync(string title, string description, string paths);
        List<videos> ListVideos();
        videos ModifyVideo(videos videos);
        string DestroyVideo(int id);

    }
}

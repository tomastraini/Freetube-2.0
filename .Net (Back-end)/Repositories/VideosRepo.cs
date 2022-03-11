using REST.Contexts;
using REST.DTOs;
using REST.Models;
using REST.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Repositories
{
    public class VideosRepo : IVideosRepo
    {
        public readonly ContextoGeneral contexto;
        public VideosRepo(ContextoGeneral contexto)
        {
            this.contexto = contexto;
        }

        public string DestroyVideo(int id)
        {
            var destroy = (from vid in contexto.Videos
                           where vid.id_video == id
                           select vid).FirstOrDefault();

            var fileDestroy = (from vid in contexto.Videos
                           where vid.id_video == id
                           select new videos() { 
                            paths = vid.paths
                           
                           }).FirstOrDefault();

            var paths = destroy.paths;
            try
            {
                File.Delete(paths);
                contexto.Videos.Remove(destroy);
                contexto.SaveChanges();
                return "Eliminación exitosa!";
            }catch(Exception e)
            {
                return "No se ha podido eliminar!";
            }
        }

        public List<videosDTO> ListVideos()
        {
            return (from vid in contexto.Videos
                            join us in contexto.users on vid.id_user equals us.id_user
                            select new videosDTO()
                            { 
                                id_video = vid.id_video,
                                description = vid.description,
                                paths = vid.paths,
                                title = vid.title,
                                usern = us.usern
                            }).ToList();
        }

        public videos ModifyVideo(videos videos)
        {
            var bd = (from vid in contexto.Videos
                      where vid.id_video == videos.id_video
                      select vid).FirstOrDefault();

            bd.paths = videos.paths;
            bd.title = videos.title;
            bd.description = videos.description;
            bd.id_user = videos.id_user;

            contexto.SaveChanges();

            return videos;
        }

        public videos OneVideo(int id)
        {
            return (from vid in contexto.Videos
                    where vid.id_video == id
                    select vid).FirstOrDefault();
        }

        public videos UploadBD(string title, string description, string path, int id_user)
        {
            try
            {
                var videos = new videos();
                videos.description = description;
                videos.paths = path;
                videos.title = title;
                videos.id_user = id_user;
                contexto.Videos.Add(new videos()
                {
                    description = description,
                    paths = path,
                    title = title,
                    id_user = id_user
                });
                contexto.SaveChanges();
                return videos;
            }catch(Exception e)
            {
                e.InnerException.ToString();
                return null;
            }
        }

        public videos watchVideo(int id)
        {
            var response = new videos();
            response = (from vid in contexto.Videos
                        where vid.id_video == id
                        select vid).FirstOrDefault();

            return response;
        }
    }
}

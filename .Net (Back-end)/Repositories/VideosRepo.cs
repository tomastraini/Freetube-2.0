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

            var commentsRelated = (from com in contexto.comments
                                   where com.id_video == id
                                   select com).ToList();

            var likesRelated = (from lik in contexto.likes
                                where lik.id_video == id
                                select lik).ToList();

            var viewedVideos = (from vw in contexto.views
                                where vw.id_video == id
                                select vw).ToList();

            var paths = destroy.paths;
            try
            {
                if(commentsRelated != null && commentsRelated.Count() > 0)
                {
                    commentsRelated.ForEach(cr => { 
                        contexto.comments.Remove(cr);
                    });
                    contexto.SaveChanges();
                }
                if (likesRelated != null && likesRelated.Count() > 0)
                {
                    likesRelated.ForEach(lr => { 
                        contexto.likes.Remove(lr);
                    });
                    contexto.SaveChanges();
                }
                if(viewedVideos != null && viewedVideos.Count() > 0)
                {
                    viewedVideos.ForEach(vw =>{
                        contexto.views.Remove(vw);
                    });
                    contexto.SaveChanges();
                }

                contexto.Videos.Remove(destroy);

                contexto.SaveChanges();
                File.Delete(paths);
                return "Eliminación exitosa!";
            }
            catch(Exception e)
            {
                return "No se ha podido eliminar!";
            }
        }

        public List<videosDTO> ListVideos()
        {
            var response = (from vid in contexto.Videos
                            join us in contexto.users on vid.id_user equals us.id_user
                            select new videosDTO()
                            { 
                                id_video = vid.id_video,
                                description = vid.description,
                                id_user = vid.id_user,
                                paths = vid.paths,
                                title = vid.title,
                                usern = us.usern,
                                fecha_carga = vid.fecha_carga
                            }).ToList();
            var likeTable = (from lik in contexto.likes
                             select lik);
            var views = (from view in contexto.views
                         select view).ToList();
            response.ForEach(r => {
                r.likes = likeTable.Where(lt => lt.id_video == r.id_video && lt.liked == true).Count();
                r.dislikes = likeTable.Where(lt => lt.id_video == r.id_video && lt.liked == false).Count();
                r.views = views.Where(v => v.id_video == r.id_video).Count();
            });

            return response;
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

        public videosDTO OneVideo(int id)
        {
            var query = (from vid in contexto.Videos
                    where vid.id_video == id
                    select vid).FirstOrDefault();
            if(query == null) { return null; }
            var likeTable = (from lik in contexto.likes
                             select lik);
            var views = (from view in contexto.views
                         where view.id_video == query.id_video
                         select view).Count();
            var response = new videosDTO()
            {
                id_video = query.id_video,
                description = query.description,
                likes = likeTable.Where(lt => lt.id_video == query.id_video && lt.liked == true).Count(),
                dislikes = likeTable.Where(lt => lt.id_video == query.id_video && lt.liked == false).Count(),
                views = views,
                id_user = query.id_user,
                title = query.title,
                usern = query.id_user.ToString(),
                fecha_carga = query.fecha_carga,
            };

            return response;
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
                    id_user = id_user,
                    fecha_carga = DateTime.Now
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

        public void deleteLike(likes like)
        {
            var exists = (from lik in contexto.likes
                          where lik.id_video == like.id_video &&
                          lik.id_user == like.id_user
                          select lik).FirstOrDefault();
            if (exists != null)
            {
                contexto.likes.Remove(exists);
                contexto.SaveChanges();
            }
        }

        public int getIfLiked(likes like)
        {
            var exists = (from lik in contexto.likes
                          where lik.id_video == like.id_video &&
                          lik.id_user == like.id_user
                          select lik).FirstOrDefault();
            if (exists == null)
            {
                return 3;
            }
            if(exists.liked == true) { return 1; } else { return 0; }
        }

        public void likeVideo(likes like)
        {
            var exists = (from lik in contexto.likes
                          where lik.id_video == like.id_video &&
                          lik.id_user == like.id_user
                          select lik).FirstOrDefault();
            if (exists == null)
            {
                contexto.likes.Add(new likes()
                {
                    id_user = like.id_user,
                    id_video = like.id_video,
                    liked = like.liked
                });
                contexto.SaveChanges();
            }
            else
            {
                exists.liked = like.liked;
                contexto.SaveChanges();
            }
        }

        public void AddViews(int id_video, int id_user)
        {
            var views = new viewedVideos()
            {
                id_user = id_user,
                id_video = id_video
            };
            contexto.views.Add(views);
            contexto.SaveChanges();
        }

        public List<videosDTO> ListTopVideos()
        {
            var response = (from vid in contexto.Videos
                            join us in contexto.users on vid.id_user equals us.id_user
                            select new videosDTO()
                            {
                                id_video = vid.id_video,
                                description = vid.description,
                                id_user = vid.id_user,
                                paths = vid.paths,
                                title = vid.title,
                                usern = us.usern,
                                fecha_carga = vid.fecha_carga
                            }).ToList();
            var likeTable = (from lik in contexto.likes
                             select lik);
            var views = (from view in contexto.views
                         select view).ToList();
            response.ForEach(r => {
                r.likes = likeTable.Where(lt => lt.id_video == r.id_video && lt.liked == true).Count();
                r.dislikes = likeTable.Where(lt => lt.id_video == r.id_video && lt.liked == false).Count();
                r.views = views.Where(v => v.id_video == r.id_video).Count();
            });

            response = response.OrderByDescending(r => r.views).ToList();
            response = response.OrderByDescending(r => r.likes).ToList();
            
            response = response.Take(8).ToList();


            return response;
        }
    }
}

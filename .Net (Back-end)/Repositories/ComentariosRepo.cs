using REST.Contexts;
using REST.DTOs;
using REST.Models;
using REST.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Repositories
{
    public class ComentariosRepo : IComentariosRepo
    {
        public readonly ContextoGeneral ctx;
        public ComentariosRepo(ContextoGeneral ctx)
        {
            this.ctx = ctx;
        }
        public List<commentsDTO> getComentarios()
        {
            var model = new List<comments>();
            var DTO = new List<commentsDTO>();

            var res = (from commens in ctx.comments 
                       join vid in ctx.Videos on commens.id_video equals vid.id_video
                       join user in ctx.users on commens.id_user equals user.id_user
                       select new commentsDTO()
                       {
                           id_comment = commens.id_comment,
                           comment = commens.comment,
                           usern = user.usern,
                           title = vid.title
                       }).ToList();
            foreach(var r in res)
            {
                DTO.Add(new commentsDTO()
                {
                    id_comment = r.id_comment,
                    comment = r.comment,
                    usern = r.usern,
                    title = r.title
                });
            };

            return DTO;
        }

        public CommentsBody modificarComentario(CommentsBody comments)
        {
            var query = (from com in ctx.comments
                         where com.id_comment == comments.id_comment
                         select com).FirstOrDefault();
            if(query == null) { return null; }
            query.comment = comments.comment;
            ctx.SaveChanges();

            return comments;
        }

        public comments postComentarios(comments comments)
        {
            try
            {
                var model = new comments();


                ctx.comments.Add(new comments()
                {
                    id_video = comments.id_video,
                    id_user = comments.id_user,
                    comment = comments.comment
                });
                ctx.SaveChanges();

                return comments;
            }catch(Exception e)
            {
                return null;
            }
        }
    }
}

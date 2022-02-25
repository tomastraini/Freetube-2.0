using REST.Contexts;
using REST.DTOs;
using REST.Models;
using REST.Repositories.Interfaces;
using REST.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Servicios
{
    public class ComentariosService : IComentariosService
    {
        public readonly IComentariosRepo repo;
        public ComentariosService(IComentariosRepo repo)
        {
            this.repo = repo;
        }
        public List<commentsDTO> getComentarios()
        {
            var response = new List<commentsDTO>();
            response = repo.getComentarios();

            return response;
        }

        public List<commentsDTO> getComentariosperVid(int id_video)
        {
            return repo.getComentariosperVid(id_video);
        }

        public CommentsBody modificarComentario(CommentsBody comments)
        {
            var response = repo.modificarComentario(comments);
            return response;
        }

        public comments postComentarios(comments comments)
        {
            var response = comments;
            response = repo.postComentarios(response);

            return response;
        }
    }
}

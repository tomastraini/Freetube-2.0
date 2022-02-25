using REST.DTOs;
using REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Repositories.Interfaces
{
    public interface IComentariosRepo
    {
        public List<commentsDTO> getComentarios();
        public List<commentsDTO> getComentariosperVid(int id_video);
        public comments postComentarios(comments comments);
        public CommentsBody modificarComentario(CommentsBody comments);
    }
}

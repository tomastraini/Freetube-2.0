using REST.DTOs;
using REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Servicios.Interfaces
{
    public interface IComentariosService
    {
        public List<commentsDTO> getComentarios();

        public comments postComentarios(comments comments);

        public CommentsBody modificarComentario(CommentsBody comments);

    }
}

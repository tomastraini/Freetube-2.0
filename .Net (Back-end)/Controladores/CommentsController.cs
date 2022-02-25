using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REST.Contexts;
using REST.DTOs;
using REST.Models;
using REST.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        public readonly ContextoGeneral contexto;
        public readonly IComentariosService comentariosService;
        public CommentsController(ContextoGeneral contexto, IComentariosService comentariosService)
        {
            this.contexto = contexto;
            this.comentariosService = comentariosService;
        }

        [HttpGet]
        public ActionResult getComentarios()
        {
            return Ok(comentariosService.getComentarios());
        }

        [HttpGet("{id_video}")]
        public ActionResult getComentarios(int id_video)
        {
            return Ok(comentariosService.getComentariosperVid(id_video));
        }

        [HttpPost]
        public ActionResult postComentarios([FromBody] comments comments)
        {
            return Ok(comentariosService.postComentarios(comments));
        }
        [HttpPatch]
        public ActionResult modificarComentario([FromBody] CommentsBody comments)
        {
            return Ok(comentariosService.modificarComentario(comments));
        }

    }
}

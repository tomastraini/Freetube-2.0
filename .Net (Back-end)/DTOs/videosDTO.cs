using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.DTOs
{
    public class videosDTO
    {
        public int id_video { get; set; }
        public string paths { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string usern { get; set; }
        public int likes { get; set; }
        public int dislikes { get; set; }
        public int id_user { get; set; }
    }
}

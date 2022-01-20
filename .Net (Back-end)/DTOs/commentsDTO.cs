using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.DTOs
{
    public class commentsDTO
    {
        public int id_comment { get; set; }
        public string comment { get; set; }
        public string usern { get; set; }
        public string title { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Models
{
    public class comments
    {
        [Key]
        public int id_comment { get; set; }
        
        public string comment { get; set; }

        public int id_user { get; set; }

        public int id_video { get; set; }
    }
}

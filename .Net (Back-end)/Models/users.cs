using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Models
{
    public class users
    {
        [Key]
        public int id_user { get; set; }

        public string usern { get; set; }

        public string passwordu { get; set; }
        public string imagepath { get; set; }
    }
}

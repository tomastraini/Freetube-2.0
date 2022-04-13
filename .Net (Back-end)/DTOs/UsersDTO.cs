using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.DTOs
{
    public class UsersDTO
    {
        public int id_user { get; set; }
        public string usern { get; set; }
        public string passwordu { get; set; }
        public string rol { get; set; }
    }
}

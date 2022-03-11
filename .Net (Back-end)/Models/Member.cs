using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool userEnc { get; set; }
        public bool passEnc { get; set; }

    }
}

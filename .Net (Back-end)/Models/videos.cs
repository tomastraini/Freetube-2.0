﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Models
{
    public class videos
    {
        public int id_video { get; set; }

        public string paths { get; set; }

        public string title { get; set; }

        public string description { get; set; }

    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApplication.Models
{
    public class Person
    {
        public long id { get; set; }
        public string name { get; set; }
        public string displayName  { get; set; }
        public List<Skill> skills { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApplication.Models
{
    public class Skill
    {
        public int id { get; set; }
        public string name  { get; set; }
        public byte level { get; set; }
        public long PersonId { get;  set; }
    }
}

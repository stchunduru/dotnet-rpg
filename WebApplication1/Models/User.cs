using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }

        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public List<Character> Characters { get; set; }

    }
}

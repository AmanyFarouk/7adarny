using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Domin.Entities
{
    public class Branch :BaseEntity
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }

        public ICollection<Group>? Groups { get; set; }
        public Teacher Teacher { get; set; }
    }
}

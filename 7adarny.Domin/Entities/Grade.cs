using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Domin.Entities
{
    public class Grade :BaseEntity
    {
        public string GradeName { get; set; }
        public int TeacherId { get; set; }

        public Teacher? Teacher { get; set; }
        public ICollection<Group>? Groups { get; set; }
    }
}

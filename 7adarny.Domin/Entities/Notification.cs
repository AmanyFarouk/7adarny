using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Domin.Entities
{
    public class Notification:BaseEntity
    {
        public int TeacherId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }=false;
        public string? ActionUrl { get; set; }


        public Teacher? Teacher { get; set; }
    }
}

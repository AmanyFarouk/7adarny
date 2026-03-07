using _7adarny.Domin.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Domin.Entities
{
    public class Attendance:BaseEntity
    {
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public DateOnly Date { get; set; }
        public AttendanceStatus Status { get; set; }

        public Student Student { get; set; }
        public Group Group { get; set; }
    }
}

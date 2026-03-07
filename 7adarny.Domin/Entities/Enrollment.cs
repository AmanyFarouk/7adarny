using _7adarny.Domin.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Domin.Entities
{
    public class Enrollment:BaseEntity
    {
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public DateTime? ResponsedAt { get; set; }
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Pending;

        public Student Student { get; set; }
        public Group Group { get; set; }
    }
}

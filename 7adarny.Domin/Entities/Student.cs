using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Domin.Entities
{
    public class Student:BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public bool IsVerified { get; set; }= false;

        public ICollection<Attendance>? Attendances { get; set; }
        public ICollection<OtpVerficiation>? OtpVerficiations { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}

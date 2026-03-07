using _7adarny.Domin.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Domin.Entities
{
    public class Teacher:BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Description { get; set; }
        public string? QualificationDocumentPath { get; set; }
        public string PasswordHash { get; set; }
        public TeacherStatus Status { get; set; }=TeacherStatus.PendingApproval;
        public string? QrCode {  get; set; }
        public bool ReceiveWeeklyReport { get; set; } = false;
        public string? ForgetPasswordToken { get; set; }
        public DateTime? ForgetPasswordTokenExpiry { get; set; }

        public ICollection<Branch>? Branches { get; set; }
        public ICollection<Grade>? Grades { get; set; }
        public ICollection<Group>? Groups { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<Report>? Reports { get; set; }

    }
}

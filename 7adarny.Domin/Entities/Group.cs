using _7adarny.Domin.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Domin.Entities
{
    public class Group:BaseEntity
    {
        public int TeacherId { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; }
        public int GradeId {  get; set; }
        public GenderType Gender { get; set; }
        public DayOfWeek Day {  get; set; }
        public int Capacity { get; set; }
        public int CurrentEnrolled { get; set; } = 0;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsActive { get; set; }=true;
        public int RemainingCapacity =>Capacity-CurrentEnrolled;
        public Teacher Teacher { get; set; }
        public Branch Branch { get; set; }
        public Grade Grade { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }
    }
}

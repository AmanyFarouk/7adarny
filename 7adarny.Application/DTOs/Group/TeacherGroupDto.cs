using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.DTOs.Group
{
    public class TeacherGroupDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string GradeName { get; set; } = string.Empty;
        public string Day { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int GroupCount { get; set; }  // CurrentEnrolled
        public int RemainingCapacity { get; set; }
        public bool IsActive { get; set; }
    }
}

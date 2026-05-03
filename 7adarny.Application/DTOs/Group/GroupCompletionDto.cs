using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.DTOs.Group
{
    public class GroupCompletionDto
    {
        public string GroupName { get; set; } = string.Empty;
        public string GroupDay { get; set; } = string.Empty;
        public int CurrentEnrolled { get; set; }
        public int Capacity { get; set; }
        public decimal CompletionPercentage { get; set; }
        public string GradeName { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.DTOs.Group
{
    public class GroupDailyEnrollmentDto
    {
        public string DayName { get; set; } = string.Empty;
        public int EnrollmentCount { get; set; }
        public int Capacity { get; set; }
    }
}

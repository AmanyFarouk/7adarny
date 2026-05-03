using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.DTOs.Group
{
    public class TeacherGroupsSummaryDto
    {
        public int FullGroups { get; set; }
        public int ActiveGroups { get; set; }
        public int GroupsCount { get; set; }
        public int StudentsCount { get; set; }
        public decimal CompletionPercentage { get; set; }
    }
}

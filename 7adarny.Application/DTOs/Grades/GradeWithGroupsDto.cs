using _7adarny.Application.DTOs.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.DTOs.Grades
{
    public class GradeWithGroupsDto
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; } = string.Empty;
        public List<GroupLookupDto> Groups { get; set; } = new();
    }
}

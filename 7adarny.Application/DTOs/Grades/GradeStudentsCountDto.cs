using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.DTOs.Grades
{
    public class GradeStudentsCountDto
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; } = string.Empty;
        public int StudentsCount { get; set; }
    }
}

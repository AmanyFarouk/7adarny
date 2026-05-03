using _7adarny.Application.DTOs.Grades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.DTOs.Teacher
{
    public class TeacherPublicProfileDto
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public string? SubjectName { get; set; }
        public string? Description { get; set; }
        public List<GradeWithGroupsDto> Grades { get; set; } = new();
    }
}

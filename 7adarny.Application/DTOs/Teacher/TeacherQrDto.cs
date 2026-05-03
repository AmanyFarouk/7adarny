using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.DTOs.Teacher
{
    public class TeacherQrDto
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public string? SubjectName { get; set; }
        public string? Description { get; set; }
        public string QrImageBase64 { get; set; } = string.Empty;
    }
}

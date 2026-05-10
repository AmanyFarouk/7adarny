using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Students.Commands.StudentLogin
{
    public class StudentLoginHandlerOutput
    {
        public bool IsSuccess { get; set; }
        public bool RequiresOtp { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Token { get; set; }
    }
}

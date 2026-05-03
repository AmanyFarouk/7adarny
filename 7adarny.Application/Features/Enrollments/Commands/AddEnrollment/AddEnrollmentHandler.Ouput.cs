using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Enrollments.Commands.AddEnrollment
{
    public class AddEnrollmentHandlerOutput
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }=string.Empty;
        public int EnrollmentId { get; set; }
    }
}

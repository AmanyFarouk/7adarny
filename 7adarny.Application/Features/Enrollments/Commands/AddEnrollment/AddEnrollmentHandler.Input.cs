using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Enrollments.Commands.AddEnrollment
{
    public class AddEnrollmentHandlerInput
    {
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public int TeacherId { get; set; }
    }
}

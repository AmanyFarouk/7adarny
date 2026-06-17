using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Enrollments.Queries.GetEnrollmentById
{
    public class GetEnrollmentByIdHandlerInput
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public GetEnrollmentByIdHandlerInput(){}
    }
}

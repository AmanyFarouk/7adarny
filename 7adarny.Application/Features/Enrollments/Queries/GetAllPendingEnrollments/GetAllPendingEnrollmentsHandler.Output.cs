using _7adarny.Application.DTOs;
using _7adarny.Application.DTOs.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Enrollments.Queries.GetAllPendingEnrollments
{
    public class GetAllPendingEnrollmentsHandlerOutput
    {
        public List<AllPendingEnrollmentDto> Enrollments { get; set; } = new();

        public int PendingCount { get; set; }

        public GetAllPendingEnrollmentsHandlerOutput(){}
    }
}

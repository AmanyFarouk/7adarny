using _7adarny.Application.DTOs.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Enrollments.Queries.GetAllEnrollments
{
    public class GetAllEnrollmentsHandlerOutput
    {
        public List<AllEnrollmentDto> Enrollments { get; set; } = new();

        public int PendingCount { get; set; }
        public int AcceptedCount { get; set; }
        public int RejectedCount { get; set; }

        public GetAllEnrollmentsHandlerOutput(){}
    }
}

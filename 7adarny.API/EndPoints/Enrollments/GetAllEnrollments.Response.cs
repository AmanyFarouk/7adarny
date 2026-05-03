using _7adarny.Application.DTOs.Enrollment;
using Microsoft.AspNetCore.Mvc;

namespace _7adarny.API.EndPoints.Enrollments
{
    public class GetAllEnrollmentsResponse
    {
        public List<AllEnrollmentDto> Enrollments { get; set; } = new();

        public int PendingCount { get; set; }
        public int AcceptedCount { get; set; }
        public int RejectedCount { get; set; }

        public GetAllEnrollmentsResponse() { }
    }
}

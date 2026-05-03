using _7adarny.Application.DTOs.Enrollment;
using Microsoft.AspNetCore.Mvc;

namespace _7adarny.API.EndPoints.Enrollments
{
    public class GetAllPendingEnrollmentsResponse
    {
        public List<AllPendingEnrollmentDto> Enrollments { get; set; } = new();

        public int PendingCount { get; set; }

        public GetAllPendingEnrollmentsResponse() { }
    }
}

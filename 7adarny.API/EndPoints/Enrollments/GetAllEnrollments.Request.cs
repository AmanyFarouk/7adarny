using Microsoft.AspNetCore.Mvc;

namespace _7adarny.API.EndPoints.Enrollments
{
    public class GetAllEnrollmentsRequest
    {
        public const string Route = "/api/enrollments";

        [FromQuery]
        public int? GradeId { get; set; }
        [FromQuery]
        public string? Status { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace _7adarny.API.EndPoints.Enrollments
{
    public class AddEnrollmentRequest
    {
        public const string Route = "/api/enrollments";

        public int GroupId { get; set; }
        public int TeacherId { get; set; }
    }
}

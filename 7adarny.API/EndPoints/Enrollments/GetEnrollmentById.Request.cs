using Microsoft.AspNetCore.Mvc;

namespace _7adarny.API.EndPoints.Enrollments
{
    public class GetEnrollmentByIdRequest
    {
        public const string Route = "/api/enrollments/{id}";
        [FromRoute]
        public int Id { get; set; }
    }
}

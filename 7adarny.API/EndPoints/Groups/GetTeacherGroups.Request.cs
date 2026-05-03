using Microsoft.AspNetCore.Mvc;

namespace _7adarny.API.EndPoints.Groups
{
    public class GetTeacherGroupsRequest
    {
        public const string Route = "/api/teacher/groups";

        [FromQuery]
        public int? GradeId { get; set; }

        [FromQuery]
        public string? Gender { get; set; }   // "Boys" / "Girls" / "Mixed"

        [FromQuery]
        public bool? IsActive { get; set; }
    }
}

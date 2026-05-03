using Microsoft.AspNetCore.Mvc;

namespace _7adarny.API.EndPoints.Teachers
{
    public class GetTeacherPublicProfileRequest
    {
        public const string Route = "/api/teachers/public-profile/{teacherId}";

        [FromRoute]
        public int TeacherId { get; set; }
    }
}

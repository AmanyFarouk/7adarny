using _7adarny.Application.DTOs.Group;

namespace _7adarny.API.EndPoints.Groups
{
    public class GetTeacherGroupsSummaryResponse
    {
        public TeacherGroupsSummaryDto Summary { get; set; } = new();
    }
}

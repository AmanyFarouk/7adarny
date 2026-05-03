using _7adarny.Application.DTOs.Group;

namespace _7adarny.API.EndPoints.Groups
{
    public class GetTeacherGroupsResponse
    {
        public List<TeacherGroupDto> Groups { get; set; } = new();
    }
}

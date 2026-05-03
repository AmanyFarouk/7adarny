using _7adarny.Application.DTOs.Group;

namespace _7adarny.API.EndPoints.Groups
{
    public class GetAllGroupsResponse
    {
        public List<GroupDto> Groups { get; set; } = new();
    }
}

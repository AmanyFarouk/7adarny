using _7adarny.Application.DTOs.Group;

namespace _7adarny.API.EndPoints.Groups
{
    public class GetGroupsCompletionResponse
    {
        public List<GroupCompletionDto> Groups { get; set; } = new();

        public GetGroupsCompletionResponse(){ }
    }
}

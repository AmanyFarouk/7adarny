using _7adarny.Application.DTOs;
namespace _7adarny.API.EndPoints.Students
{
    public class GetGroupStudentsResponse
    {
        public List<GroupStudentsDto> Students { get; set; } = new();
        public GetGroupStudentsResponse(){ }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace _7adarny.API.EndPoints.Students
{
    public class GetGroupStudentsRequest
    {
        public const string Route = "/api/groups/{groupId}/students";

        [FromRoute]
        public int GroupId { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace _7adarny.API.EndPoints.Groups
{
    public class GetGroupsCompletionRequest
    {
        public const string Route = "/api/statistics/groups-completion";

        [FromQuery]
        public int GradeId { get; set; }
    }
}

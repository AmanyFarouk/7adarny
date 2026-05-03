using Microsoft.AspNetCore.Mvc;

namespace _7adarny.API.EndPoints.Groups
{
    public class GetGroupEnrollmentStatsRequest
    {
        public const string Route = "/api/statistics/group/{groupId}/daily-enrollments";

        [FromRoute]
        public int GroupId { get; set; }
    }
}

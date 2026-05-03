using _7adarny.Application.DTOs.Group;

namespace _7adarny.API.EndPoints.Groups
{
    public class GetGroupEnrollmentStatsResponse
    {
        public List<GroupDailyEnrollmentDto> DailyStats { get; set; } = new();
    }
}

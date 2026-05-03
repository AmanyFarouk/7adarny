using _7adarny.Application.Features.Groupss.Queries.GetGroupEnrollmentStats;
using AutoMapper;

namespace _7adarny.API.EndPoints.Groups
{
    public class GetGroupEnrollmentStatsMapper:Profile
    {
        public GetGroupEnrollmentStatsMapper()
        {
            CreateMap<GetGroupEnrollmentStatsRequest, GetGroupEnrollmentStatsHandlerInput>()
            .ForMember(dest => dest.TeacherId, opt => opt.Ignore())
            .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId));

            CreateMap<GetGroupEnrollmentStatsHandlerOutput, GetGroupEnrollmentStatsResponse>()
                .ForMember(dest => dest.DailyStats, opt => opt.MapFrom(src => src.DailyStats));

        }
    }
}

using _7adarny.Application.Features.Groupss.Queries.GetTeacherGroupsSummary;
using AutoMapper;

namespace _7adarny.API.EndPoints.Groups
{
    public class GetTeacherGroupsSummaryMapper:Profile
    {
        public GetTeacherGroupsSummaryMapper()
        {
            CreateMap<GetTeacherGroupsSummaryHandlerOutput, GetTeacherGroupsSummaryResponse>()
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary));
        }
    }
}

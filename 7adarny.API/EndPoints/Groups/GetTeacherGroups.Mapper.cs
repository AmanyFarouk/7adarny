using _7adarny.Application.Features.Groupss.Queries.GetTeacherGroups;
using AutoMapper;

namespace _7adarny.API.EndPoints.Groups
{
    public class GetTeacherGroupsMapper:Profile
    {
        public GetTeacherGroupsMapper()
        {
            CreateMap<GetTeacherGroupsRequest, GetTeacherGroupsHandlerInput>()
                .ForMember(dest => dest.TeacherId, opt => opt.Ignore())
                .ForMember(dest => dest.GradeId, opt => opt.MapFrom(src => src.GradeId))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));

            CreateMap<GetTeacherGroupsHandlerOutput, GetTeacherGroupsResponse>()
                .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.Groups));
        }
    }
}

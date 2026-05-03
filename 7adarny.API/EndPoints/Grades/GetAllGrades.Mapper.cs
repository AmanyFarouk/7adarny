using _7adarny.API.EndPoints.Groups;
using _7adarny.Application.Features.Grades.Queries.GetAllGrades;
using _7adarny.Application.Features.Groupss.Queries.GetAllGroups;
using AutoMapper;

namespace _7adarny.API.EndPoints.Grades
{
    public class GetAllGradesMapper:Profile
    {
        public GetAllGradesMapper()
        {
            CreateMap<GetAllGradesRequest, GetAllGradesHandlerInput>()
                .ForMember(dest => dest.TeacherId, opt => opt.Ignore());
            CreateMap<GetAllGradesHandlerOutput, GetAllGradesResponse>()
                .ForMember(dest => dest.Grades, opt => opt.MapFrom(src => src.Grades));
        }
    }
}

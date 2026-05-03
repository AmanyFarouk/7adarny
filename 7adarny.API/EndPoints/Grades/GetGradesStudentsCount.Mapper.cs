using _7adarny.Application.Features.Grades.Queries.GetGradesStudentsCount;
using AutoMapper;

namespace _7adarny.API.EndPoints.Grades
{
    public class GetGradesStudentsCountMapper:Profile
    {
        public GetGradesStudentsCountMapper()
        {
            CreateMap<GetGradesStudentsCountHandlerOutput, GetGradesStudentsCountResponse>()
            .ForMember(dest => dest.Grades, opt => opt.MapFrom(src => src.Grades));
        }
    }
}

using _7adarny.Application.Features.Teachers.Queries.GetTeacherProfile;
using AutoMapper;

namespace _7adarny.API.EndPoints.Teachers
{
    public class GetTeacherProfileMapper:Profile
    {
        public GetTeacherProfileMapper()
        {
            CreateMap<GetTeacherProfileRequest, GetTeacherProfileHandlerInput>()
                .ForMember(dest => dest.TeacherId, opt => opt.Ignore());

            CreateMap<GetTeacherProfileHandlerOutput, GetTeacherProfileResponse>()
                .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => src.Teacher));
        }
    }
}

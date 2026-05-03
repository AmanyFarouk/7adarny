using _7adarny.Application.Features.Teachers.Queries.GetTeacherPublicProfile;
using AutoMapper;

namespace _7adarny.API.EndPoints.Teachers
{
    public class GetTeacherPublicProfileMapper:Profile
    {
        public GetTeacherPublicProfileMapper()
        {
            CreateMap<GetTeacherPublicProfileRequest, GetTeacherPublicProfileHandlerInput>()
            .ForMember(dest => dest.TeacherId, opt => opt.MapFrom(src => src.TeacherId));

            CreateMap<GetTeacherPublicProfileHandlerOutput, GetTeacherPublicProfileResponse>()
                .ForMember(dest => dest.Profile, opt => opt.MapFrom(src => src.Profile));
        }
    }
}

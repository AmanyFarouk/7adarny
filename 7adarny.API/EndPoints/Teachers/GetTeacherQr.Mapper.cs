using _7adarny.Application.Features.Teachers.Queries.GetTeacherQr;
using AutoMapper;

namespace _7adarny.API.EndPoints.Teachers
{
    public class GetTeacherQrMapper:Profile
    {
        public GetTeacherQrMapper()
        {
            CreateMap<GetTeacherQrHandlerOutput, GetTeacherQrResponse>()
           .ForMember(dest => dest.TeacherQr, opt => opt.MapFrom(src => src.TeacherQr));
        }
    }
}

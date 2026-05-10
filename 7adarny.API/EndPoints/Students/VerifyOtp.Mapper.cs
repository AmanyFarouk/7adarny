using _7adarny.Application.Features.Students.Commands.VerifyOtp;
using AutoMapper;

namespace _7adarny.API.EndPoints.Students
{
    public class VerifyOtpMapper:Profile
    {
        public VerifyOtpMapper()
        {
            CreateMap<VerifyOtpRequest, VerifyOtpHandlerInput>();
            CreateMap<VerifyOtpHandlerOutput, VerifyOtpResponse>();
        }
    }
}

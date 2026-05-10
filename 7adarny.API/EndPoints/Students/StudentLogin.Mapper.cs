using _7adarny.Application.Features.Students.Commands.StudentLogin;
using AutoMapper;

namespace _7adarny.API.EndPoints.Students
{
    public class StudentLoginMapper:Profile
    {
        public StudentLoginMapper()
        {
            CreateMap<StudentLoginRequest, StudentLoginHandlerInput>();
            CreateMap<StudentLoginHandlerOutput, StudentLoginResponse>();
        }
    }
}

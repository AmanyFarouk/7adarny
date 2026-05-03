using _7adarny.Application.Features.Enrollments.Commands.AddEnrollment;
using AutoMapper;

namespace _7adarny.API.EndPoints.Enrollments
{
    public class AddEnrollmentMapper:Profile
    {
        public AddEnrollmentMapper()
        {
            CreateMap<AddEnrollmentRequest, AddEnrollmentHandlerInput>()
               .ForMember(dest => dest.StudentId, opt => opt.Ignore());

            CreateMap<AddEnrollmentHandlerOutput, AddEnrollmentResponse>();
        }
    }
}

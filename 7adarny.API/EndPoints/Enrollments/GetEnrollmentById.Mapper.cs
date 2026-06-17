using _7adarny.Application.Features.Enrollments.Queries.GetEnrollmentById;
using AutoMapper;

namespace _7adarny.API.EndPoints.Enrollments
{
    public class GetEnrollmentByIdMapper :Profile
    {
        public GetEnrollmentByIdMapper()
        {
           CreateMap<GetEnrollmentByIdRequest, GetEnrollmentByIdHandlerInput>()
               .ForMember(dest => dest.EnrollmentId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.StudentId, opt => opt.Ignore());
           CreateMap<GetEnrollmentByIdHandlerOutput, GetEnrollmentByIdResponse>();
        }
    }
}

using _7adarny.Application.Features.Enrollments.Queries.GetAllPendingEnrollments;
using AutoMapper;

namespace _7adarny.API.EndPoints.Enrollments
{
    public class GetAllPendingEnrollmentsMapper : Profile
    {
        public GetAllPendingEnrollmentsMapper()
        {
            CreateMap<GetAllPendingEnrollmentsRequest, GetAllPendingEnrollmentsHandlerInput>()
            .ForMember(dest => dest.TeacherId, opt => opt.Ignore());

            CreateMap<GetAllPendingEnrollmentsHandlerOutput, GetAllPendingEnrollmentsResponse>()
            .ForMember(dest => dest.Enrollments, opt => opt.MapFrom(src => src.Enrollments))
            .ForMember(dest => dest.PendingCount, opt => opt.MapFrom(src => src.PendingCount));

        }
    }
}

using _7adarny.Application.Features.Enrollments.Queries.GetAllEnrollments;
using AutoMapper;

namespace _7adarny.API.EndPoints.Enrollments
{
    public class GetAllEnrollmentsMapper : Profile
    {
        public GetAllEnrollmentsMapper()
        {
            CreateMap<GetAllEnrollmentsRequest, GetAllEnrollmentsHandlerInput>()
            .ForMember(dest => dest.TeacherId, opt => opt.Ignore())
            .ForMember(dest => dest.GradeId, opt => opt.MapFrom(src => src.GradeId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<GetAllEnrollmentsHandlerOutput, GetAllEnrollmentsResponse>()
            .ForMember(dest => dest.Enrollments, opt => opt.MapFrom(src => src.Enrollments))
            .ForMember(dest => dest.PendingCount, opt => opt.MapFrom(src => src.PendingCount))
            .ForMember(dest => dest.AcceptedCount, opt => opt.MapFrom(src => src.AcceptedCount))
            .ForMember(dest => dest.RejectedCount, opt => opt.MapFrom(src => src.RejectedCount));

        }
    }
}

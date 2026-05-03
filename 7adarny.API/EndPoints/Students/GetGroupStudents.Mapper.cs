using _7adarny.Application.DTOs;
using _7adarny.Application.Features.Students.Queries.GetGroupStudents;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _7adarny.API.EndPoints.Students
{
    public class GetGroupStudentsMapper : Profile
    {
        public GetGroupStudentsMapper()
        {
            CreateMap<GetGroupStudentsRequest, GetGroupStudentsHandlerInput>()
                .ForMember(dest => dest.TeacherId, opt => opt.Ignore());

            CreateMap<GetGroupStudentsHandlerOutput, GetGroupStudentsResponse>()
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students));

        }
    }
}

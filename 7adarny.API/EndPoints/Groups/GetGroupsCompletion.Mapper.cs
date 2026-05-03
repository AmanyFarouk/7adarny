using _7adarny.Application.Features.Groupss.Queries.GetGroupsCompletion;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _7adarny.API.EndPoints.Groups
{
    public class GetGroupsCompletionMapper:Profile
    {
        public GetGroupsCompletionMapper()
        {
            CreateMap<GetGroupsCompletionRequest, GetGroupsCompletionHandlerInput>()
                .ForMember(dest => dest.TeacherId, opt => opt.Ignore())
                .ForMember(dest => dest.GradeId, opt => opt.MapFrom(src => src.GradeId));

            CreateMap<GetGroupsCompletionHandlerOutput, GetGroupsCompletionResponse>()
                .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.Groups));
        }
    }
}

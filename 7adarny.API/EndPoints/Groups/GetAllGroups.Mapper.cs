using _7adarny.Application.Features.Groupss.Queries.GetAllGroups;
using AutoMapper;

namespace _7adarny.API.EndPoints.Groups
{
    public class GetAllGroupsMapper:Profile
    {
        public GetAllGroupsMapper()
        {
            CreateMap<GetAllGroupsRequest, GetAllGroupsHandlerInput>()
                .ForMember(dest=>dest.TeacherId,opt=>opt.Ignore());
            CreateMap<GetAllGroupsHandlerOutput, GetAllGroupsResponse>()
                .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.Groups));
        }
    }
}

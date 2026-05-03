using _7adarny.API.EndPoints.Groups;
using _7adarny.Application.Features.Branches.Queries.GetAllBranches;
using _7adarny.Application.Features.Groupss.Queries.GetAllGroups;
using AutoMapper;

namespace _7adarny.API.EndPoints.Branches
{
    public class GetAllBranchesMapper : Profile
    {
        public GetAllBranchesMapper()
        {
            CreateMap<GetAllBranchesRequest, GetAllGroupsHandlerInput>()
                .ForMember(dest => dest.TeacherId, opt => opt.Ignore());
            CreateMap<GetAllBranchesHandlerOutput, GetAllBranchesResponse>()
                .ForMember(dest => dest.Branches, opt => opt.MapFrom(src => src.Branches));
        }
    }
}

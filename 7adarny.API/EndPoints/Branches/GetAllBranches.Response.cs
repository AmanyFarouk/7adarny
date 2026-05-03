using _7adarny.Application.DTOs.Branches;

namespace _7adarny.API.EndPoints.Branches
{
    public class GetAllBranchesResponse
    {
        public List<BranchDto> Branches { get; set; } = new();
    }
}

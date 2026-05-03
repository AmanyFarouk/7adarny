using _7adarny.Application.DTOs.Branches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Branches.Queries.GetAllBranches
{
    public class GetAllBranchesHandlerOutput
    {
        public List<BranchDto> Branches { get; set; } = new();
    }
}

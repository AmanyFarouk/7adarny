using _7adarny.Application.DTOs.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Groupss.Queries.GetAllGroups
{
    public class GetAllGroupsHandlerOutput
    {
        public List<GroupDto> Groups { get; set; } = new();
    }
}

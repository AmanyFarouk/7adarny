using _7adarny.Application.DTOs.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Groupss.Queries.GetTeacherGroups
{
    public class GetTeacherGroupsHandlerOutput
    {
        public List<TeacherGroupDto> Groups { get; set; } = new();
    }
}

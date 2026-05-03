using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Groupss.Queries.GetTeacherGroups
{
    public class GetTeacherGroupsHandlerInput
    {
        public int TeacherId { get; set; }
        public int? GradeId { get; set; }
        public string? Gender { get; set; }
        public bool? IsActive { get; set; }

    }
}

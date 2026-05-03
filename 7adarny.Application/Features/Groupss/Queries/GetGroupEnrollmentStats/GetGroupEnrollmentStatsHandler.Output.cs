using _7adarny.Application.DTOs.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Groupss.Queries.GetGroupEnrollmentStats
{
    public class GetGroupEnrollmentStatsHandlerOutput
    {
        public List<GroupDailyEnrollmentDto> DailyStats { get; set; } = new();
    }
}

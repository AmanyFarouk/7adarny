using _7adarny.Application.DTOs.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Teachers.Queries.GetTeacherPublicProfile
{
    public class GetTeacherPublicProfileHandlerOutput
    {
        public TeacherPublicProfileDto? Profile { get; set; }
    }
}

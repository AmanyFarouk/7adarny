using _7adarny.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Students.Queries.GetGroupStudents
{
    public class GetGroupStudentsHandlerOutput
    {
        public List<GroupStudentsDto> Students { get; set; } = new();

        public GetGroupStudentsHandlerOutput(){}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Students.Queries.GetGroupStudents
{
    public class GetGroupStudentsHandlerInput
    {
        public int TeacherId { get; set; }
        public int GroupId { get; set; }

        public GetGroupStudentsHandlerInput(){}
    }
}

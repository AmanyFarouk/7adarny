using _7adarny.Application.DTOs.Grades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Grades.Queries.GetGradesStudentsCount
{
    public class GetGradesStudentsCountHandlerOutput
    {
        public List<GradeStudentsCountDto> Grades { get; set; } = new();
    }
}

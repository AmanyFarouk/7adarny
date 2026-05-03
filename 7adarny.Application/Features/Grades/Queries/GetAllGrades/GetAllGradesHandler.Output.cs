using _7adarny.Application.DTOs.Grades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Grades.Queries.GetAllGrades
{
    public class GetAllGradesHandlerOutput
    {
        public List<GradeDto> Grades { get; set; } = new();
    }
}

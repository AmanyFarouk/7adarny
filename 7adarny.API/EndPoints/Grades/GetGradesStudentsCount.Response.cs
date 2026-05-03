using _7adarny.Application.DTOs.Grades;

namespace _7adarny.API.EndPoints.Grades
{
    public class GetGradesStudentsCountResponse
    {
        public List<GradeStudentsCountDto> Grades { get; set; } = new();
    }
}

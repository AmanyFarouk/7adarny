using _7adarny.Application.DTOs.Grades;

namespace _7adarny.API.EndPoints.Grades
{
    public class GetAllGradesResponse
    {
        public List<GradeDto> Grades { get; set; } = new();
    }
}

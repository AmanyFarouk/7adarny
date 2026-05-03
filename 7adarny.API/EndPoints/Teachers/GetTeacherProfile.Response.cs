using _7adarny.Application.DTOs.Teacher;

namespace _7adarny.API.EndPoints.Teachers
{
    public class GetTeacherProfileResponse
    {
        public TeacherProfileDto? Teacher { get; set; }

        public GetTeacherProfileResponse(){}
    }
}

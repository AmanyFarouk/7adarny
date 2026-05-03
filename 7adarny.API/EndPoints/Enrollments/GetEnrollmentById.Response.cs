using _7adarny.Application.DTOs.Enrollment;

namespace _7adarny.API.EndPoints.Enrollments
{
    public class GetEnrollmentByIdResponse
    {
        public EnrollmentDto Enrollment { get; set; } = new();
    }
}

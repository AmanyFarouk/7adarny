namespace _7adarny.API.EndPoints.Enrollments
{
    public class AddEnrollmentResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } =string.Empty;
        public int EnrollmentId { get; set; }
    }
}

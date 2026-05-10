namespace _7adarny.API.EndPoints.Students
{
    public class StudentLoginRequest
    {
        public const string Route = "/api/auth/student/login";
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}

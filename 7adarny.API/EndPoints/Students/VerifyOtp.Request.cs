namespace _7adarny.API.EndPoints.Students
{
    public class VerifyOtpRequest
    {
        public const string Route = "/api/auth/student/verify-otp";
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string OtpCode { get; set; } = string.Empty;
    }
}

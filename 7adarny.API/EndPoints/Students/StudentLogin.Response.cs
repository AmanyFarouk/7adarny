namespace _7adarny.API.EndPoints.Students
{
    public class StudentLoginResponse
    {
        public bool IsSuccess { get; set; }
        public bool RequiresOtp { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Token { get; set; }
    }
}

namespace _7adarny.API.EndPoints.Students
{
    public class VerifyOtpResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Token { get; set; }
    }
}

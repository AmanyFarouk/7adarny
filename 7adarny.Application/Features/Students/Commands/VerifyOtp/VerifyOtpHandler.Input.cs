using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Students.Commands.VerifyOtp
{
    public class VerifyOtpHandlerInput
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string OtpCode { get; set; } = string.Empty;
    }
}

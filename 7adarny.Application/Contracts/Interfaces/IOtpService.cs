using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Contracts.Interfaces
{
    public interface IOtpService
    {
        Task SendOtpAsync(string phone);
        Task<bool> VerifyOtpAsync(string phone, string code);
    }
}

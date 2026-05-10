using _7adarny.Application.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Verify.V2.Service;

namespace _7adarny.Infrastructure.Services
{
    public class TwilioOtpService : IOtpService
    {
        private readonly ILogger<TwilioOtpService> _logger;
        private readonly string _serviceSid;
        public TwilioOtpService(IConfiguration config,
            ILogger<TwilioOtpService> logger)
        {
            _logger = logger;

            TwilioClient.Init(config["Twilio:AccountSid"], config["Twilio:AuthToken"]);

            _serviceSid = config["Twilio:VerifyServiceSid"]!;
        }
        public async Task SendOtpAsync(string phone)
        {
            try
            {
                await VerificationResource.CreateAsync(
                    to: phone,
                    channel: "sms",
                    pathServiceSid: _serviceSid);

                _logger.LogInformation(
                    "OTP sent via Twilio Verify to {Phone}", phone);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Failed to send OTP to {Phone}: {Error}",
                    phone, ex.Message);

                throw;
            }
        }

        public async Task<bool> VerifyOtpAsync(string phone, string code)
        {
            try
            {
                var result = await VerificationCheckResource.CreateAsync(
                    to: phone,
                    code: code,
                    pathServiceSid: _serviceSid);

                _logger.LogInformation(
                    "OTP verification result for {Phone}: {Status}",
                    phone, result.Status);

                return result.Status == "approved";
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "OTP verification failed for {Phone}: {Error}",
                    phone, ex.Message);

                return false;
            }
        }
    }
}

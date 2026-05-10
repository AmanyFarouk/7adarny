using _7adarny.Application.Contracts;
using _7adarny.Application.Contracts.Interfaces;
using _7adarny.Domin.Entities;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Students.Commands.StudentLogin
{
    public class StudentLoginHandler : IStudentLoginHandlerContract<StudentLoginHandlerInput, StudentLoginHandlerOutput>
    {
        private readonly IDbConnectionFactory _connection;
        private readonly IOtpService _otpService;
        private readonly IJwtService _jwt;
        private readonly ILogger<StudentLoginHandler> _logger;

        public StudentLoginHandler(IDbConnectionFactory connection,
                                   IOtpService otpService,
                                   IJwtService jwt,
                                   ILogger<StudentLoginHandler> logger)
        {
            _connection = connection;
            _otpService = otpService;
            _jwt = jwt;
            _logger = logger;
        }
        public async Task<StudentLoginHandlerOutput> HandleAsync(StudentLoginHandlerInput input, CancellationToken cancellationToken)
        {
            using var connection = _connection.CreateConnection();

            _logger.LogInformation(
                "Login attempt for phone: {Phone}", input.Phone);

            //check if student already exists
            var studentSql = @"
            SELECT Id, Name, Phone, IsVerified
            FROM Students
            WHERE Phone   = @Phone
            AND IsDeleted = 0";

            var student = await connection
                .QueryFirstOrDefaultAsync<Student>(
                    studentSql, new { input.Phone });

            // if student exists and verified
            if (student is not null && student.IsVerified)
            {
                _logger.LogInformation(
                    "Student {Phone} already verified, logging in", input.Phone);

                return new StudentLoginHandlerOutput
                {
                    IsSuccess = true,
                    RequiresOtp = false,
                    Message = "Login successful",
                    Token = _jwt.GenerateToken(
                        student.Id, "Student", student.Name)
                };
            }

            //if student not exitsts or isverified=false
            await _otpService.SendOtpAsync(input.Phone);

            _logger.LogInformation(
                "OTP sent to {Phone}", input.Phone);

            return new StudentLoginHandlerOutput
            {
                IsSuccess = true,
                RequiresOtp = true,
                Message = "OTP sent successfully"
            };
        }
    }
}

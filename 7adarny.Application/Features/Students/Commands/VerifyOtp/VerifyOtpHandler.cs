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

namespace _7adarny.Application.Features.Students.Commands.VerifyOtp
{
    public class VerifyOtpHandler : IVerifyOtpHandlerContract<VerifyOtpHandlerInput, VerifyOtpHandlerOutput>
    {
        private readonly IDbConnectionFactory _connection;
        private readonly IGenericRepository<Student> _studentRepo;
        private readonly IOtpService _otpService;
        private readonly IJwtService _jwt;
        private readonly ILogger<VerifyOtpHandler> _logger;

        public VerifyOtpHandler(IDbConnectionFactory connection,
                                IGenericRepository<Student> studentRepo,
                                IOtpService otpService,
                                IJwtService jwt,
                                ILogger<VerifyOtpHandler> logger)
        {
            _connection = connection;
            _studentRepo = studentRepo;
            _otpService = otpService;
            _jwt = jwt;
            _logger = logger;
        }

        public async Task<VerifyOtpHandlerOutput> HandleAsync(VerifyOtpHandlerInput input, CancellationToken cancellationToken)
        {
            using var connection = _connection.CreateConnection();

            _logger.LogInformation(
                "OTP verification attempt for {Phone}", input.Phone);

            // 1. verify with twilio
            var isValid = await _otpService.VerifyOtpAsync(
                input.Phone, input.OtpCode);

            if (!isValid)
            {
                _logger.LogWarning(
                    "Invalid OTP for {Phone}", input.Phone);

                return new VerifyOtpHandlerOutput
                {
                    IsSuccess = false,
                    Message = "Invalid or expired OTP"
                };
            }

            // 2. get student
            var studentSql = @"
            SELECT Id, Name, Phone, IsVerified
            FROM Students
            WHERE Phone   = @Phone
            AND IsDeleted = 0";

            var student = await connection
                .QueryFirstOrDefaultAsync<Student>(
                    studentSql, new { input.Phone });

            // 3. if student is not found create new student
            if (student is null)
            {
                _logger.LogInformation(
                    "Creating new student for {Phone}", input.Phone);

                student = new Student
                {
                    Name = input.Name,
                    Phone = input.Phone,
                    IsVerified = true
                };

                await _studentRepo.AddAsync(student);
                await _studentRepo.SaveChangesAsync();
            }
            else
            {
                // 4. student exists -> set isVerified = true
                student.IsVerified = true;
                _studentRepo.Update(student);
                await _studentRepo.SaveChangesAsync();
            }

            _logger.LogInformation(
                "Student {Phone} verified and logged in", input.Phone);

            // 5. Generate JWT
            return new VerifyOtpHandlerOutput
            {
                IsSuccess = true,
                Message = "Login successful",
                Token = _jwt.GenerateToken(
                    student.Id, "Student", student.Name)
            };
        }
    }
}

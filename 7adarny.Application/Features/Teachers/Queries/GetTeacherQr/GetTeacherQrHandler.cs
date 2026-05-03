using _7adarny.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using _7adarny.Application.DTOs.Teacher;
using QRCoder;

namespace _7adarny.Application.Features.Teachers.Queries.GetTeacherQr
{
    public class GetTeacherQrHandler : IGetTeacherQrHandlerContract<GetTeacherQrHandlerInput, GetTeacherQrHandlerOutput>
    {
        private readonly IDbConnectionFactory _connection;
        private readonly IConfiguration _config;

        public GetTeacherQrHandler(IDbConnectionFactory connection
            , IConfiguration config)
        {
            _connection = connection;
            _config = config;
        }
        public async Task<GetTeacherQrHandlerOutput> HandleAsync(GetTeacherQrHandlerInput input, CancellationToken cancellationToken)
        {
            var output=new GetTeacherQrHandlerOutput();

            var sql = @" SELECT
                         t.Id          As TeacherId,
                         t.Name        AS TeacherName,
                         t.SubjectName AS SubjectName,
                         t.Description AS Description
                         FROM Teachers t
                         WHERE
                         t.Id        = @TeacherId
                         AND t.IsDeleted = 0
                         AND t.Status    = 'Approved'";
            using var connection = _connection.CreateConnection();

            var teacher = await connection.QueryFirstOrDefaultAsync<TeacherQrDto>(sql, new
            {
                input.TeacherId
            });

            if (teacher is null)
                return new GetTeacherQrHandlerOutput();

            var baseurl= _config["Frontend:PublicBaseUrl"];

            var qrCodeUrl = $"{baseurl}api/teachers/public-profile/{input.TeacherId}";

            using var qrGenerator = new QRCodeGenerator();
            var qrData = qrGenerator.CreateQrCode(qrCodeUrl, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrData);
            var base64 = Convert.ToBase64String(qrCode.GetGraphic(20));

            teacher.QrImageBase64 = $"data:image/png;base64,{base64}";
            output.TeacherQr=teacher;
            return output;
        }
    }
}

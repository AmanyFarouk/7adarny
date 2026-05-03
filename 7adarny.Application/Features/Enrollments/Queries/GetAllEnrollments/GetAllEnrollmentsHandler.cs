using _7adarny.Application.Contracts;
using _7adarny.Application.DTOs;
using _7adarny.Application.DTOs.Enrollment;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Enrollments.Queries.GetAllEnrollments
{
    public class GetAllEnrollmentsHandler : IGetAllEnrollmentsHandlerContract<GetAllEnrollmentsHandlerInput, GetAllEnrollmentsHandlerOutput>
    {
        private readonly IDbConnectionFactory _connection;
        public GetAllEnrollmentsHandler(IDbConnectionFactory connection)
        {
            _connection = connection;
        }
        public async Task<GetAllEnrollmentsHandlerOutput> HandleAsync(GetAllEnrollmentsHandlerInput input, CancellationToken cancellationToken)
        {
            var output= new GetAllEnrollmentsHandlerOutput();
            var sql = @"SELECT
                        e.Id          AS EnrollmentId,
                        s.Id          AS StudentId,
                        s.Name        AS StudentName,
                        s.Phone       AS StudentPhone,
                        g.Id          AS GroupId,
                        g.Name        AS GroupName,
                        gr.GradeName  AS GradeName,
                        e.CreatedAt   AS RequestedAt,
                        e.Status      AS Status
                        FROM  Enrollments e
                        INNER JOIN Students s  ON s.Id  = e.StudentId
                        INNER JOIN Groups   g  ON g.Id  = e.GroupId
                        INNER JOIN Grades   gr ON gr.Id = g.GradeId
                        WHERE
                        g.TeacherId = @TeacherId
                        AND e.IsDeleted = 0
                        AND s.IsDeleted = 0
                        AND g.IsDeleted = 0
                        AND (@GradeId IS NULL OR g.GradeId = @GradeId)
                        AND (@Status  IS NULL OR e.Status  = @Status)
                        ORDER BY e.CreatedAt DESC";
            using var connection = _connection.CreateConnection();
            var result = await connection.QueryAsync<AllEnrollmentDto>(sql,new
            {
                input.TeacherId,
                input.GradeId, 
                input.Status
            });

            output.Enrollments = result.ToList();

            output.PendingCount = output.Enrollments.Count(e => e.Status == "Pending");
            output.AcceptedCount = output.Enrollments.Count(e => e.Status == "Accepted");
            output.RejectedCount = output.Enrollments.Count(e => e.Status == "Rejected"); 
            return output;
        }
    }
}

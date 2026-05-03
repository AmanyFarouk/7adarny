using _7adarny.Application.Contracts;
using _7adarny.Application.DTOs.Enrollment;
using _7adarny.Application.Features.Enrollments.Queries.GetAllPendingEnrollments;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Enrollments.Queries.GetAllEnrollments
{
    public class GetAllPendingEnrollmentsHandler : IGetAllPendingEnrollmentsHandlerContract<GetAllPendingEnrollmentsHandlerInput, GetAllPendingEnrollmentsHandlerOutput>
    {
        private readonly IDbConnectionFactory _connection;
        public GetAllPendingEnrollmentsHandler(IDbConnectionFactory connection)
        {
            _connection = connection;
        }
        public async Task<GetAllPendingEnrollmentsHandlerOutput> HandleAsync(GetAllPendingEnrollmentsHandlerInput input, CancellationToken cancellationToken)
        {
            var output= new GetAllPendingEnrollmentsHandlerOutput();
            var sql = @"SELECT
                        e.Id          AS EnrollmentId,
                        s.Id          AS StudentId,
                        s.Name        AS StudentName,
                        g.Id          AS GroupId,
                        g.Name        AS GroupName,
                        gr.GradeName  AS GradeName,
                        e.CreatedAt   AS RequestedAt
                        FROM  Enrollments e
                        INNER JOIN Students s  ON s.Id  = e.StudentId
                        INNER JOIN Groups   g  ON g.Id  = e.GroupId
                        INNER JOIN Grades   gr ON gr.Id = g.GradeId
                        WHERE
                        g.TeacherId = @TeacherId
                        AND e.Status='Pending'
                        AND e.IsDeleted = 0
                        AND s.IsDeleted = 0
                        AND g.IsDeleted = 0
                        ORDER BY e.CreatedAt DESC";
            using var connection = _connection.CreateConnection();
            var result = await connection.QueryAsync<AllPendingEnrollmentDto>(sql,new
            {
                input.TeacherId
            });

            output.Enrollments = result.ToList();

            output.PendingCount = output.Enrollments.Count(); 
            return output;
        }
    }
}

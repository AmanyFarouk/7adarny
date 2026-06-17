using _7adarny.Application.Contracts;
using _7adarny.Application.DTOs.Enrollment;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Enrollments.Queries.GetEnrollmentById
{
    public class GetEnrollmentByIdHandler : IGetEnrollmentByIdHandlerContract<GetEnrollmentByIdHandlerInput, GetEnrollmentByIdHandlerOutput>
    {
        private readonly IDbConnectionFactory _connection;

        public GetEnrollmentByIdHandler(IDbConnectionFactory connection)
        {
            _connection = connection;
        }
        public async Task<GetEnrollmentByIdHandlerOutput> HandleAsync(GetEnrollmentByIdHandlerInput input, CancellationToken cancellationToken)
        {
            var output= new GetEnrollmentByIdHandlerOutput();
            var sql = @"SELECT 
                          s.Name AS StudentName,
                          gr.GradeName AS GradeName,
                          g.Name AS GroupName,
                          t.SubjectName AS SubjectName,
                          g.Day AS Day,
                          g.StartTime AS StartTime,
                          g.EndTime AS EndTime,
                          e.Status AS Status
                          FROM Enrollments e 
                          INNER JOIN Students s ON s.Id=e.StudentId
                          INNER JOIN Groups g ON g.Id=e.GroupId
                          INNER JOIN Grades gr ON gr.Id=g.GradeId
                          INNER JOIN Teachers t ON t.Id=g.TeacherId
                          WHERE e.Id=@EnrollmentId
                          AND e.StudentId =@StudentId";
            using var connection = _connection.CreateConnection();
            var result =await connection.QueryFirstOrDefaultAsync<EnrollmentDto>(sql, new
            {
               input.EnrollmentId,
               input.StudentId
            });

            output.Enrollment = result;
            return output;
        }
    }
}

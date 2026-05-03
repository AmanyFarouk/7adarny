using _7adarny.Application.Contracts;
using _7adarny.Application.DTOs.Grades;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Grades.Queries.GetGradesStudentsCount
{
    internal class GetGradesStudentsCountHandler : IGetGradesStudentsCountHandlerContract<GetGradesStudentsCountHandlerInput, GetGradesStudentsCountHandlerOutput>
    {
        private readonly IDbConnectionFactory _connection;

        public GetGradesStudentsCountHandler(IDbConnectionFactory connection)
        {
            _connection = connection;
        }
        public async Task<GetGradesStudentsCountHandlerOutput> HandleAsync(GetGradesStudentsCountHandlerInput input, CancellationToken cancellationToken)
        {
            var output = new GetGradesStudentsCountHandlerOutput();

            var sql = @"SELECT
                        gr.Id                   AS GradeId,
                        gr.GradeName            AS GradeName,
                        COUNT(DISTINCT e.StudentId) AS StudentsCount
                        FROM Grades gr
                        INNER JOIN Groups     g ON g.GradeId   = gr.Id
                        INNER JOIN Enrollments e ON e.GroupId  = g.Id
                        WHERE
                        gr.TeacherId    = @TeacherId
                        AND e.Status    = 'Accepted'
                        AND e.IsDeleted = 0
                        AND g.IsDeleted = 0
                        AND gr.IsDeleted = 0
                        GROUP BY
                        gr.Id,
                        gr.GradeName
                        ORDER BY
                        gr.GradeName";
                
            using var connection = _connection.CreateConnection();

            var result = await connection.QueryAsync<GradeStudentsCountDto>(sql,new
            { input.TeacherId });

            output.Grades = result.ToList();
            return output;
        }
    }
}

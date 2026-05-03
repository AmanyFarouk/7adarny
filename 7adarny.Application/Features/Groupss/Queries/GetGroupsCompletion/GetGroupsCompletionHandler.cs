using _7adarny.Application.Contracts;
using _7adarny.Application.DTOs.Group;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Groupss.Queries.GetGroupsCompletion
{
    public class GetGroupsCompletionHandler : IGetGroupsCompletionHandlerContract<GetGroupsCompletionHandlerInput, GetGroupsCompletionHandlerOutput>
    {
        private readonly IDbConnectionFactory _connection;
        public GetGroupsCompletionHandler(IDbConnectionFactory connection)
        {
            _connection = connection;
        }

        public async Task<GetGroupsCompletionHandlerOutput> HandleAsync(GetGroupsCompletionHandlerInput input, CancellationToken cancellationToken)
        {
            var output = new GetGroupsCompletionHandlerOutput();

            var sql = @" SELECT
                      g.Name  AS GroupName,
                      g.Day AS GroupDay,
                      g.CurrentEnrolled AS CurrentEnrolled,
                      g.Capacity  AS Capacity,
                      CAST(
                          CAST(g.CurrentEnrolled AS DECIMAL(5,2)) / g.Capacity * 100
                      AS DECIMAL(5,0))  AS CompletionPercentage,
                      gr.GradeName                            AS GradeName,
                      CONVERT(VARCHAR(5), g.StartTime, 108)   AS StartTime,
                      CONVERT(VARCHAR(5), g.EndTime,   108)   AS EndTime,
                      g.Gender                                AS Gender
                      FROM Groups g
                      INNER JOIN Grades gr ON gr.Id = g.GradeId
                      WHERE
                      g.TeacherId = @TeacherId
                      AND g.GradeId   = @GradeId
                      AND g.IsDeleted = 0
                      ORDER BY g.Name";
            using var connection = _connection.CreateConnection();
            var result = await connection.QueryAsync<GroupCompletionDto>(sql, new
            {
                input.TeacherId,
                input.GradeId
            });
            output.Groups = result.ToList();

            return output;
        }
    }
}

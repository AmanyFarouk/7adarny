using _7adarny.Application.Contracts;
using _7adarny.Application.DTOs.Group;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Groupss.Queries.GetTeacherGroupsSummary
{
    public class GetTeacherGroupsSummaryHandler : IGetTeacherGroupsSummaryHandlerContract<GetTeacherGroupsSummaryHandlerInput, GetTeacherGroupsSummaryHandlerOutput>
    {
        private readonly IDbConnectionFactory _connection;
        public GetTeacherGroupsSummaryHandler(IDbConnectionFactory connection)
        {
            _connection = connection;
        }
        public async Task<GetTeacherGroupsSummaryHandlerOutput> HandleAsync(GetTeacherGroupsSummaryHandlerInput input, CancellationToken cancellationToken)
        {
            var output = new GetTeacherGroupsSummaryHandlerOutput();

            var sql = @"SELECT
                        SUM(CASE WHEN g.CurrentEnrolled  = g.Capacity THEN 1 ELSE 0 END) AS FullGroups,
                        SUM(CASE WHEN g.IsActive = 1 
                        AND g.CurrentEnrolled < g.Capacity    THEN 1 ELSE 0 END) AS ActiveGroups,
                        COUNT(g.Id) AS GroupsCount,
                        SUM(g.CurrentEnrolled) AS StudentsCount,
                        CAST(
                        CAST(SUM(g.CurrentEnrolled) AS DECIMAL(10,2))
                        / NULLIF(SUM(g.Capacity), 0) * 100
                        AS DECIMAL(5,2))  AS CompletionPercentage
                        FROM Groups g
                        WHERE
                        g.TeacherId = @TeacherId
                        AND g.IsDeleted = 0";

            using var connection = _connection.CreateConnection();

            var result = await connection.QueryFirstOrDefaultAsync<TeacherGroupsSummaryDto>(sql,new
                { input.TeacherId });

            output.Summary = result;

            return output;
        }
    }
}

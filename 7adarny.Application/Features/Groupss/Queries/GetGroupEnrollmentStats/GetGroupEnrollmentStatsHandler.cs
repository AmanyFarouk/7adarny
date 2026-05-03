using _7adarny.Application.Contracts;
using _7adarny.Application.DTOs.Group;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Groupss.Queries.GetGroupEnrollmentStats
{
    public class GetGroupEnrollmentStatsHandler : IGetGroupEnrollmentStatsHandlerContract<GetGroupEnrollmentStatsHandlerInput, GetGroupEnrollmentStatsHandlerOutput>
    {
        private readonly IDbConnectionFactory _connection;

        public GetGroupEnrollmentStatsHandler(IDbConnectionFactory connection)
        {
            _connection = connection;
        }
        public async Task<GetGroupEnrollmentStatsHandlerOutput> HandleAsync(GetGroupEnrollmentStatsHandlerInput input, CancellationToken cancellationToken)
        {
            var output = new GetGroupEnrollmentStatsHandlerOutput();

            var sql = @"SELECT
                        DATENAME(WEEKDAY, e.CreatedAt)  AS DayName,
                        COUNT(e.Id)                     AS EnrollmentCount,
                        g.Capacity                      AS Capacity
                        FROM Enrollments e
                        INNER JOIN Groups g ON g.Id = e.GroupId
                        WHERE
                        e.GroupId   = @GroupId
                        AND g.TeacherId = @TeacherId
                        AND e.IsDeleted = 0
                        AND g.IsDeleted = 0
                        GROUP BY
                        DATENAME(WEEKDAY, e.CreatedAt),
                        DATEPART(WEEKDAY, e.CreatedAt),
                        g.Capacity
                        ORDER BY
                        DATEPART(WEEKDAY, e.CreatedAt)";
            using var connection = _connection.CreateConnection();

            var result = await connection.QueryAsync<GroupDailyEnrollmentDto>(sql,new 
                  { input.GroupId, input.TeacherId });

            output.DailyStats = result.ToList();
            return output;
        }
    }
}

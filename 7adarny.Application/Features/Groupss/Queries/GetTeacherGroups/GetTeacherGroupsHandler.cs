using _7adarny.Application.Contracts;
using _7adarny.Application.DTOs.Group;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Groupss.Queries.GetTeacherGroups
{
    public class GetTeacherGroupsHandler : IGetTeacherGroupsHandlerContract<GetTeacherGroupsHandlerInput, GetTeacherGroupsHandlerOutput>
    {
        private readonly IDbConnectionFactory _connection;
        public GetTeacherGroupsHandler(IDbConnectionFactory connection)
        {
            _connection = connection;
        }
        public async Task<GetTeacherGroupsHandlerOutput> HandleAsync(GetTeacherGroupsHandlerInput input, CancellationToken cancellationToken)
        {
            var output = new GetTeacherGroupsHandlerOutput();

            var sql = @"SELECT
                       g.Id     AS GroupId,
                       g.Name     AS GroupName,
                       g.Gender      AS Gender,
                       gr.GradeName  AS GradeName,
                       g.Day     AS Day,
                       CONVERT(VARCHAR(5), g.StartTime, 108)     AS StartTime,
                       CONVERT(VARCHAR(5), g.EndTime,   108)   AS EndTime,
                       g.Capacity   AS Capacity,
                       g.CurrentEnrolled AS GroupCount,
                       (g.Capacity - g.CurrentEnrolled)   AS RemainingCapacity,
                       g.IsActive     AS IsActive
                       FROM  Groups g
                       INNER JOIN Grades gr ON gr.Id = g.GradeId
                       WHERE
                       g.TeacherId  = @TeacherId
                       AND g.IsDeleted  = 0
                       AND (@GradeId  IS NULL OR g.GradeId = @GradeId)
                       AND (@Gender   IS NULL OR g.Gender  = @Gender)
                       AND (@IsActive IS NULL OR g.IsActive = @IsActive)
                       ORDER BY g.Name";
            using var connection = _connection.CreateConnection();

            var result = await connection.QueryAsync<TeacherGroupDto>(sql, new
            {
                input.TeacherId,
                input.GradeId,
                input.Gender,
                input.IsActive
            });

            output.Groups = result.ToList();
            return output;
        }
    }
}

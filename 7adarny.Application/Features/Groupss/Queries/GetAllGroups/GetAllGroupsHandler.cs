using _7adarny.Application.Contracts;
using _7adarny.Application.DTOs.Group;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Groupss.Queries.GetAllGroups
{
    public class GetAllGroupsHandler : IGetAllGroupsHandlerContract<GetAllGroupsHandlerInput, GetAllGroupsHandlerOutput>
    {
        private readonly IDbConnectionFactory _connection;

        public GetAllGroupsHandler(IDbConnectionFactory connection)
        {
            _connection = connection;
        }

        public async Task<GetAllGroupsHandlerOutput> HandleAsync(GetAllGroupsHandlerInput input, CancellationToken cancellationToken)
        {
            var output = new GetAllGroupsHandlerOutput();

            var sql = @"SELECT
                        g.Id AS GroupId, 
                        g.Name AS GroupName
                        FROM Groups g
                        WHERE g.TeacherId = @TeacherId 
                        AND g.IsActive = 1
                        AND g.IsDeleted=0
                        ORDER BY g.Name";
            using var connection = _connection.CreateConnection();
            
            var result =await connection.QueryAsync<GroupDto>(sql, new 
            { TeacherId = input.TeacherId });

            output.Groups=result.ToList();
            return output;
        }
    }
}

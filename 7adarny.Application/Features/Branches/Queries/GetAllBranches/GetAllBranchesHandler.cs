using _7adarny.Application.Contracts;
using _7adarny.Application.DTOs.Branches;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Branches.Queries.GetAllBranches
{
    public class GetAllBranchesHandler : IGetAllBranchesHandlerContract<GetAllBranchesHandlerInput, GetAllBranchesHandlerOutput>
    {
        private readonly IDbConnectionFactory _connection;

        public GetAllBranchesHandler(IDbConnectionFactory connection)
        {
            _connection = connection;
        }
        public async Task<GetAllBranchesHandlerOutput> HandleAsync(GetAllBranchesHandlerInput input, CancellationToken cancellationToken)
        {
            var output = new GetAllBranchesHandlerOutput();

            var sql = @"SELECT 
                        b.Id AS BranchId,
                        b.Name AS BranchName,
                        b.Address AS BranchAddress
                        FROM Branches b
                        WHERE b.TeacherId=@TeacherId
                        AND b.IsDeleted=0";
            using var connection =_connection.CreateConnection();
            var result =await connection.QueryAsync<BranchDto>(sql, new 
            { TeacherId = input.TeacherId });
            output.Branches = result.ToList();
            return output;
        }
    }
}

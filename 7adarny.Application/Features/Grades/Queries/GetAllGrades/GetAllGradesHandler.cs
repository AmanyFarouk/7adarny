using _7adarny.Application.Contracts;
using _7adarny.Application.DTOs.Grades;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Grades.Queries.GetAllGrades
{
    public class GetAllGradesHandler : IGetAllGradesHandlerContract<GetAllGradesHandlerInput, GetAllGradesHandlerOutput>
    {
        private readonly IDbConnectionFactory _connection;

        public GetAllGradesHandler(IDbConnectionFactory connection)
        {
            _connection = connection;
        }
        public async Task<GetAllGradesHandlerOutput> HandleAsync(GetAllGradesHandlerInput input, CancellationToken cancellationToken)
        {
            var output=new GetAllGradesHandlerOutput();

            var sql = @"SELECT 
                        gr.Id AS GradeId,
	                    gr.GradeName AS GradeName
                        FROM Grades gr
                        WHERE gr.TeacherId=@TeacherId
                        AND gr.IsDeleted=0";
            using var connection =  _connection.CreateConnection();

            var result = await connection.QueryAsync<GradeDto>(sql, new 
            { TeacherId = input.TeacherId });
            output.Grades = result.ToList();
            return output;
        }
    }
}

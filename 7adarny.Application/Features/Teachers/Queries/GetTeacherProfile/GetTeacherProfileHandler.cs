using _7adarny.Application.Contracts;
using _7adarny.Application.DTOs.Teacher;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Teachers.Queries.GetTeacherProfile
{
    public class GetTeacherProfileHandler : IGetTeacherProfileHandlerContract<GetTeacherProfileHandlerInput, GetTeacherProfileHandlerOutput>
    {
        private readonly IDbConnectionFactory _connection;
        public GetTeacherProfileHandler(IDbConnectionFactory connection)
        {
            _connection = connection;
        }

        public async Task<GetTeacherProfileHandlerOutput> HandleAsync(GetTeacherProfileHandlerInput input, CancellationToken cancellationToken)
        {
            var output=new GetTeacherProfileHandlerOutput();

            var sql = @"SELECT 
                        t.Name,
	                    t.Phone,
	                    t.SubjectName,
	                    t.Description
                        FROM Teachers t
                        WHERE t.Id=@TeacherId
                        AND t.IsDeleted = 0
                        AND t.Status    = 'Approved';";

            using var connection=_connection.CreateConnection();

            var result = await connection.QueryFirstOrDefaultAsync<TeacherProfileDto>(sql, new
            {
                input.TeacherId
            });

            output.Teacher = result;

            return output;
        }
    }
}

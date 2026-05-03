using _7adarny.Application.Contracts;
using _7adarny.Application.DTOs;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Students.Queries.GetGroupStudents
{
    public class GetGroupStudentsHandler : IGetGroupStudentsHandlerContract<GetGroupStudentsHandlerInput, GetGroupStudentsHandlerOutput>
    {
        private readonly IDbConnectionFactory _connection;
        public GetGroupStudentsHandler(IDbConnectionFactory connection)
        {
            _connection = connection;
        }
        public async Task<GetGroupStudentsHandlerOutput> HandleAsync(GetGroupStudentsHandlerInput input, CancellationToken cancellationToken)
        {
            var output = new GetGroupStudentsHandlerOutput();
            string sql = @"SELECT 
                           s.Id,
                           s.Name,
                           s.Phone
                           FROM Enrollments e
                           INNER JOIN Students s ON s.Id = e.StudentId
                           INNER JOIN Groups   g ON g.Id = e.GroupId
                           WHERE  g.TeacherId = @TeacherId
                           AND e.GroupId = @GroupId
                           AND e.Status  = 'Accepted'
                           AND s.IsDeleted = 0
                           AND g.IsDeleted = 0
                           AND e.IsDeleted = 0
                           ORDER BY s.Name;";
            using var connection =_connection.CreateConnection();
            var result =await connection.QueryAsync<GroupStudentsDto>(sql, new
            {
                input.TeacherId,
                input.GroupId
            });
            output.Students = result.ToList();
            return output;
        }
    }
}

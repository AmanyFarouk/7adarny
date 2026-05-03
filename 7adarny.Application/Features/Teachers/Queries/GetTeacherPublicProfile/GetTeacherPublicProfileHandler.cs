using _7adarny.Application.Contracts;
using _7adarny.Application.DTOs.Grades;
using _7adarny.Application.DTOs.Group;
using _7adarny.Application.DTOs.Teacher;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Teachers.Queries.GetTeacherPublicProfile
{
    public class GetTeacherPublicProfileHandler : IGetTeacherPublicProfileHandlerContract<GetTeacherPublicProfileHandlerInput, GetTeacherPublicProfileHandlerOutput>
    {
        private readonly IDbConnectionFactory _connection;

        public GetTeacherPublicProfileHandler(IDbConnectionFactory connection)
        {
            _connection = connection;
        }
        public async Task<GetTeacherPublicProfileHandlerOutput> HandleAsync(GetTeacherPublicProfileHandlerInput input, CancellationToken cancellationToken)
        {
            var output=new GetTeacherPublicProfileHandlerOutput();
            
            var teacherSql = @"SELECT
                               t.Id          AS TeacherId,
                               t.Name        AS TeacherName,
                               t.SubjectName AS SubjectName,
                               t.Description AS Description
                               FROM Teachers t
                               WHERE
                               t.Id        = @TeacherId
                               AND t.Status    = 'Approved'
                               AND t.IsDeleted = 0";

            // Query 2 — كل الـ Groups مع الـ Grade والـ Branch معلومات
            var groupsSql = @"SELECT
                              gr.Id                                AS GradeId,
                              gr.GradeName                         AS GradeName,
                              g.Id                                 AS GroupId,
                              g.Name                               AS GroupName,
                              g.Gender							   As GroupType,
                              g.Day                                AS Day,
                              CONVERT(VARCHAR(5), g.StartTime,108) AS StartTime,
                              CONVERT(VARCHAR(5), g.EndTime,  108) AS EndTime,
                              g.Capacity                           AS Capacity,
                              g.CurrentEnrolled                    AS CurrentEnrolled,
                              (g.Capacity - g.CurrentEnrolled)     AS RemainingCapacity,
                              ISNULL(b.Address, '')                AS Address
                              FROM Grades gr
                              INNER JOIN Groups  g ON g.GradeId   = gr.Id
                              INNER JOIN Branches b ON b.Id       = g.BranchId
                              WHERE gr.TeacherId = @TeacherId
                              AND g.IsActive   = 1
                              AND g.IsDeleted  = 0
                              AND gr.IsDeleted = 0
                              AND (g.Capacity - g.CurrentEnrolled) > 0
                              ORDER BY gr.GradeName, g.Name";

            using var connection = _connection.CreateConnection();

            var teacher = await connection.QueryFirstOrDefaultAsync<TeacherPublicProfileDto>(
                teacherSql, new { input.TeacherId });

            if (teacher is null)
                return new GetTeacherPublicProfileHandlerOutput();

            // Dapper بيرجع flat rows — محتاجة تعملي grouping يدوي
            var rows = await connection.QueryAsync<dynamic>(
                groupsSql, new { input.TeacherId });

            teacher.Grades = rows
                .GroupBy(r => (int)r.GradeId)
                .Select(g => new GradeWithGroupsDto
                {
                    GradeId = g.Key,
                    GradeName = (string)g.First().GradeName,
                    Groups = g.Select(r => new GroupLookupDto
                    {
                        GroupId = (int)r.GroupId,
                        GroupName = (string)r.GroupName,
                        GroupType=(string)r.GroupType,
                        Day = (string)r.Day,
                        StartTime = (string)r.StartTime,
                        EndTime = (string)r.EndTime,
                        Capacity = (int)r.Capacity,
                        CurrentEnrolled = (int)r.CurrentEnrolled,
                        RemainingCapacity = (int)r.RemainingCapacity,
                        Address = (string)r.Address
                    }).ToList()
                }).ToList();
            output.Profile = teacher;
            return output;
        }
    }
}

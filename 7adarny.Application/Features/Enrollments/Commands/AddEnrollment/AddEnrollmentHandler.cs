using _7adarny.Application.Contracts;
using _7adarny.Application.Contracts.Interfaces;
using _7adarny.Domin.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Enrollments.Commands.AddEnrollment
{
    public class AddEnrollmentHandler : IAddEnrollmentHandlerContract<AddEnrollmentHandlerInput, AddEnrollmentHandlerOutput>
    {
        private readonly IDbConnectionFactory _connection;
        private readonly IGenericRepository<Enrollment> _enrollmentRepo;
        private readonly IGenericRepository<Notification> _notificationRepo;
        private readonly IConfiguration _config;

        public AddEnrollmentHandler(IDbConnectionFactory connection
            ,IGenericRepository<Enrollment> enrollmentRepo
            ,IGenericRepository<Notification> notificationRepo
            , IConfiguration config)
        {
            _connection = connection;
            _enrollmentRepo = enrollmentRepo;
            _notificationRepo = notificationRepo;
            _config = config;
        }
        public async Task<AddEnrollmentHandlerOutput> HandleAsync(AddEnrollmentHandlerInput input, CancellationToken cancellationToken)
        {
            
            //check if group full 
            var isGroupFullSql = @"SELECT COUNT(1)
                                   FROM Groups
                                   WHERE
                                   Id        = @GroupId
                                   AND CurrentEnrolled >= Capacity
                                   AND IsDeleted = 0";
            using var connection=_connection.CreateConnection();
            var IsFull = await connection.ExecuteScalarAsync<int>(isGroupFullSql, new
            {
                input.GroupId
            });
            if(IsFull != 0)
            {
                return new AddEnrollmentHandlerOutput()
                {
                    IsSuccess = false,
                    Message = "Group Is Full"
                };
            }
            //check if the student already enrolled in this group
            var checkGroupExistsSql = @"SELECT COUNT(1)
                                     FROM Enrollments
                                     WHERE
                                     StudentId = @StudentId
                                     AND GroupId   = @GroupId
                                     AND IsDeleted = 0";
            var ifExistsGroup = await connection.ExecuteScalarAsync<int>(checkGroupExistsSql, new
            {
                input.StudentId,
                input.GroupId
            });
            if (ifExistsGroup != 0)
            {
                return new AddEnrollmentHandlerOutput()
                {
                    IsSuccess = false,
                    Message = "Student Already Enrolled in this group"
                };
            }
            //check if the student in any groups of the teacher
            var existsSql = @"SELECT COUNT(1)
                            FROM Enrollments e
                            INNER JOIN Groups g ON g.Id = e.GroupId
                            WHERE
                            e.StudentId   = @StudentId
                            AND g.TeacherId   = @TeacherId
                            AND e.Status  IN ('Pending', 'Accepted')
                            AND e.IsDeleted = 0";
            var exists = await connection.ExecuteScalarAsync<int>(existsSql, new
            {
                input.GroupId,
                input.StudentId,
                input.TeacherId
            });
            if(exists != 0)
            {
                return new AddEnrollmentHandlerOutput()
                {
                    IsSuccess = false,
                    Message = "Student Already enrolled in another group with this teacher"
                };
            }
            var newEnrollment= new Enrollment()
            {
                StudentId=input.StudentId,
                GroupId=input.GroupId,
            };

            await _enrollmentRepo.AddAsync(newEnrollment);
            await _enrollmentRepo.SaveChangesAsync();
            // 4. جيب اسم الطالب للـ Notification
            var studentNameSql = @"
            SELECT Name FROM Students
            WHERE Id = @StudentId AND IsDeleted = 0";

            var studentName = await connection.ExecuteScalarAsync<string>(
                studentNameSql, new { input.StudentId });

            // 5. جيب اسم المجموعة للـ Notification
            var groupNameSql = @"
            SELECT Name FROM Groups
            WHERE Id = @GroupId AND IsDeleted = 0";
            var groupName = await connection.ExecuteScalarAsync<string>(
            groupNameSql, new { input.GroupId });

            var baseurl = _config["Frontend:PublicBaseUrl"];

            var notification = new Notification()
            {
                TeacherId=input.TeacherId,
                Message= $"{studentName} requested to join {groupName}",
                IsRead = false,
                ActionUrl = $"{baseurl}api/enrollments/pending"
            };
            await _notificationRepo.AddAsync(notification);
            await _notificationRepo.SaveChangesAsync();
            return new AddEnrollmentHandlerOutput()
            {
                IsSuccess = true,
                Message = "Enrollment request Added Successfully",
                EnrollmentId=newEnrollment.Id
            };
        }
    }
}

using _7adarny.Domin.Entities;
using _7adarny.Domin.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Infrastructure.Data
{
    public class DataSeeder
    {
        public static async Task SeedAsync(Context db)
        {
            // if there is data in tables don't seed any data
            if (db.Teachers.Any()) return;

            // ── Teachers ──────────────────────────────────────────
            var teacher1 = new Teacher
            {
                Name = "Ahmed Hassan",
                Email = "ahmed@7adarny.com",
                Phone = "+201012345678",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test@1234"),
                Status = TeacherStatus.Approved,
                Description = "Math teacher with 10 years experience",
                QrCode = "https://frontend.com/teacher/1",
                ReceiveWeeklyReport = true
            };

            var teacher2 = new Teacher
            {
                Name = "Sara Mohamed",
                Email = "sara@7adarny.com",
                Phone = "+201112345678",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test@1234"),
                Status = TeacherStatus.PendingApproval,
                Description = "Physics teacher"
            };

            await db.Teachers.AddRangeAsync(teacher1, teacher2);
            //await db.SaveChangesAsync();

            // ── Branches ──────────────────────────────────────────
            var branch1 = new Branch
            {
                Name = "Nasr City Branch",
                Address = "15 Makram Ebeid St, Nasr City",
                TeacherId = teacher1.Id
            };

            var branch2 = new Branch
            {
                Name = "Maadi Branch",
                Address = "8 Road 9, Maadi",
                TeacherId = teacher1.Id
            };

            await db.Branches.AddRangeAsync(branch1, branch2);
            //await db.SaveChangesAsync();

            // ── Grades ────────────────────────────────────────────
            var grade1 = new Grade
            {
                GradeName = "First Secondary",
                TeacherId = teacher1.Id
            };

            var grade2 = new Grade
            {
                GradeName = "Second Secondary",
                TeacherId = teacher1.Id
            };

            await db.Grades.AddRangeAsync(grade1, grade2);
            //await db.SaveChangesAsync();

            // ── Groups ────────────────────────────────────────────
            var group1 = new Group
            {
                Name = "Math Group A",
                TeacherId = teacher1.Id,
                BranchId = branch1.Id,
                GradeId = grade1.Id,
                Gender = GenderType.Boys,
                Day = DayOfWeek.Saturday,
                Capacity = 20,
                CurrentEnrolled = 1,
                StartTime = new TimeOnly(10, 0),
                EndTime = new TimeOnly(11, 30),
                IsActive = true
            };

            var group2 = new Group
            {
                Name = "Math Group B",
                TeacherId = teacher1.Id,
                BranchId = branch2.Id,
                GradeId = grade2.Id,
                Gender = GenderType.Girls,
                Day = DayOfWeek.Monday,
                Capacity = 15,
                CurrentEnrolled = 0,
                StartTime = new TimeOnly(14, 0),
                EndTime = new TimeOnly(15, 30),
                IsActive = true
            };

            await db.Groups.AddRangeAsync(group1, group2);
            //await db.SaveChangesAsync();

            // ── Students ──────────────────────────────────────────
            var student1 = new Student
            {
                Name = "Omar Ali Hassan Khaled",
                Phone = "+201098765432",
                IsVerified = true
            };

            var student2 = new Student
            {
                Name = "Youssef Mohamed Ahmed Samir",
                Phone = "+201198765432",
                IsVerified = false  // لسه محتاج OTP
            };

            await db.Students.AddRangeAsync(student1, student2);
            //await db.SaveChangesAsync();

            // ── Enrollments ───────────────────────────────────────
            var enrollment1 = new Enrollment
            {
                StudentId = student1.Id,
                GroupId = group1.Id,
                Status = EnrollmentStatus.Accepted,
                ResponsedAt = DateTime.UtcNow
            };

            var enrollment2 = new Enrollment
            {
                StudentId = student2.Id,
                GroupId = group1.Id,
                Status = EnrollmentStatus.Pending
            };

            await db.Enrollments.AddRangeAsync(enrollment1, enrollment2);
            //await db.SaveChangesAsync();

            // ── Attendance ────────────────────────────────────────
            var attendance1 = new Attendance
            {
                StudentId = student1.Id,
                GroupId = group1.Id,
                Date = DateOnly.FromDateTime(DateTime.Today),
                Status = AttendanceStatus.Present
            };

            var attendance2 = new Attendance
            {
                StudentId = student1.Id,
                GroupId = group1.Id,
                Date = DateOnly.FromDateTime(DateTime.Today.AddDays(-7)),
                Status = AttendanceStatus.Absent
            };

            await db.Attendances.AddRangeAsync(attendance1, attendance2);
            //await db.SaveChangesAsync();

            // ── Notifications ─────────────────────────────────────
            var notification = new Notification
            {
                TeacherId = teacher1.Id,
                Message = "Omar Ali Hassan Khaled requested to join Math Group A",
                IsRead = false,
                ActionUrl = "/enrollments/pending"
            };

            await db.Notifications.AddAsync(notification);
            await db.SaveChangesAsync();
        }
    }
}

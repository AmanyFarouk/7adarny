using _7adarny.Application.DTOs.Enrollment;
using _7adarny.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Enrollments.Queries.GetEnrollmentById
{
    public class GetEnrollmentByIdHandlerOutput
    {
        public EnrollmentDto Enrollment { get; set; } = new();
    }
}

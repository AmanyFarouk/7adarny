using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Features.Enrollments.Queries.GetAllEnrollments
{
    public class GetAllEnrollmentsHandlerInput
    {
        public int TeacherId { get; set; }
        public int? GradeId { get; set; }
        public string? Status { get; set; }

        public GetAllEnrollmentsHandlerInput() { }
    }
}

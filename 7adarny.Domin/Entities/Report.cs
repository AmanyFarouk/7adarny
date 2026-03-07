using _7adarny.Domin.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Domin.Entities
{
    public class Report:BaseEntity
    {
        public int TeacherId { get; set; }
        public int? GroupId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public ReportType Type { get; set; }
        public ReportFormat Format { get; set; }
        public string? FilePath { get; set; }
        public bool WasEmailed { get; set; }
        public bool IsDownloadedManually { get; set; }

        public Teacher Teacher { get; set; }
        public Group? Group { get; set; }

    }
}

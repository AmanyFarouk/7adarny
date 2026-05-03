using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.DTOs.Branches
{
    public class BranchDto
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }=string.Empty;
        public string BranchAddress { get; set; }=string.Empty;
    }
}

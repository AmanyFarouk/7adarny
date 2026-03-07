using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Domin.Entities
{
    public class OtpVerficiation :BaseEntity
    {
        public int StudentId { get; set; }
        public string OTPCode { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime ExpiredAt { get; set; }
        public bool IsUsed { get; set; } = false;
        public bool IsValid()=>!IsUsed &&ExpiredAt >DateTime.UtcNow;

        public Student Student { get; set; }

    }
}

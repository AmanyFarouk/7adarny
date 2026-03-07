using _7adarny.Domin.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Infrastructure.Data.Configurations
{
    public class OtpVerificationConfigurations : IEntityTypeConfiguration<OtpVerficiation>
    {
        public void Configure(EntityTypeBuilder<OtpVerficiation> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.OTPCode)
                   .IsRequired()
                   .HasMaxLength(6)
                   .IsUnicode(false);

            builder.Property(o => o.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(13)
                   .IsUnicode(false);

            builder.Property(t => t.Email)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.HasOne(o => o.Student)
                   .WithMany(s => s.OtpVerficiations)
                   .HasForeignKey(o => o.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Index لتسريع البحث عن OTP برقم التليفون
            builder.HasIndex(o => new { o.PhoneNumber, o.IsUsed});
        }
    }
}

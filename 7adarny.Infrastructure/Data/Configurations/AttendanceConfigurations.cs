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
    public class AttendanceConfigurations : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20);

            builder.Property(a => a.Date)
                   .HasColumnType("date");

            // can't record attendance for one student in the same group and date more than one time
            builder.HasIndex(a => new { a.StudentId, a.GroupId, a.Date })
                   .IsUnique();

            //Relationships
            builder.HasOne(a => a.Student)
                   .WithMany(s => s.Attendances)
                   .HasForeignKey(a => a.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Group)
                   .WithMany(g => g.Attendances)
                   .HasForeignKey(a => a.GroupId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

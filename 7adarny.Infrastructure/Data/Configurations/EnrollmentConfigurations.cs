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
    public class EnrollmentConfigurations : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20);

            //student can join one group only with one teacher
            builder.HasIndex(e => new { e.StudentId, e.GroupId })
                   .IsUnique();

            //Relationships
            builder.HasOne(e => e.Student)
                   .WithMany(s => s.Enrollments)
                   .HasForeignKey(e => e.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Group)
                   .WithMany(g => g.Enrollments)
                   .HasForeignKey(e => e.GroupId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

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
    public class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasAnnotation("MinLength", 4);

            builder.HasCheckConstraint(
                    "CK_Student_Name_FourParts",
                    "LEN(Name) - LEN(REPLACE(Name, ' ', '')) >= 3"
                    );

            builder.Property(s => s.Phone)
                   .IsRequired()
                   .HasMaxLength(13)
                   .IsUnicode(false);
            builder.HasIndex(s => s.Phone)
                  .IsUnique();
            builder.HasCheckConstraint(
                    "CK_Student_Phone_Egyptian",
                    "Phone LIKE '+201[0125]%' AND LEN(Phone) = 13"
                    );

            builder.Property(t => t.Email)
                   .HasMaxLength(50)
                   .IsUnicode(false);
            builder.HasCheckConstraint(
                   "CK_Student_Email_Format",
                   "Email IS NULL OR Email LIKE '%_@_%._%'"
                    );

            //relationships
            builder.HasMany(s => s.Attendances)
                   .WithOne(a => a.Student)
                   .HasForeignKey(a => a.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.OtpVerficiations)
                   .WithOne(o => o.Student)
                   .HasForeignKey(o => o.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Enrollments)
                   .WithOne(b => b.Student)
                   .HasForeignKey(b => b.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

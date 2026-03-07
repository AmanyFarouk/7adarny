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
    public class TeacherConfigurations : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(t => t.Email)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.HasIndex(t => t.Email)
                   .IsUnique();
            builder.HasCheckConstraint(
                    "CK_Teacher_Email_Format",
                    "Email LIKE '%_@_%._%'"
                     );
         
            builder.Property(t => t.Phone)
                   .IsRequired()
                   .HasMaxLength(13)
                   .IsUnicode(false);

            builder.HasIndex(t => t.Phone)
                   .IsUnique();

            builder.HasCheckConstraint(
                    "CK_Teacher_Phone_Egyptian",
                    "Phone LIKE '+201[0125][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' AND LEN(Phone) = 13"
                    );
            
            builder.Property(t => t.Description)
                   .HasMaxLength(50);
            
            builder.Property(t => t.PasswordHash)
                   .IsRequired();
            
            builder.Property(t => t.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20);

            //relationships
            builder.HasMany(t => t.Groups)
                   .WithOne(g => g.Teacher)
                   .HasForeignKey(g => g.TeacherId)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasMany(t => t.Notifications)
                   .WithOne(n => n.Teacher)
                   .HasForeignKey(n => n.TeacherId)
                   .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(t => t.Reports)
                   .WithOne(r => r.Teacher)
                   .HasForeignKey(r => r.TeacherId)
                   .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(t=>t.Grades)
                   .WithOne(g=>g.Teacher)
                   .HasForeignKey(t => t.TeacherId)
                   .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(t => t.Branches)
                   .WithOne( b=> b.Teacher)
                   .HasForeignKey(t => t.TeacherId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

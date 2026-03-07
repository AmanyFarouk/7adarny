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
    public class ReportConfigurations : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Type)
                   .HasConversion<string>()
                   .HasMaxLength(20);

            builder.Property(r => r.Format)
                   .HasConversion<string>()
                   .HasMaxLength(10);

            builder.Property(r => r.FilePath)
                   .HasMaxLength(200);

            //Relationships
            builder.HasOne(r => r.Teacher)
                   .WithMany(t => t.Reports)
                   .HasForeignKey(r => r.TeacherId)
                   .OnDelete(DeleteBehavior.Cascade);

            // GroupId nullable — Report ممكن يكون لكل المجموعات
            builder.HasOne(r => r.Group)
                   .WithMany()
                   .HasForeignKey(r => r.GroupId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        }
    }
}

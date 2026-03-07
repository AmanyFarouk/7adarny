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
    public class GradeConfigurations : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.GradeName)
                   .IsRequired()
                   .HasMaxLength(100);

            //Relationships
            builder.HasOne(g => g.Teacher)
                   .WithMany(t => t.Grades)
                   .HasForeignKey(g => g.TeacherId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(g => g.Groups)
                   .WithOne(gr => gr.Grade)
                   .HasForeignKey(gr => gr.GradeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

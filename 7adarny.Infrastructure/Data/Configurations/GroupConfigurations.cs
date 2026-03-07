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
    public class GroupConfigurations : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(g => g.Gender)
                   .HasConversion<string>()
                   .HasMaxLength(10);
            
            builder.Property(g => g.Day)
                   .HasConversion<string>()
                   .HasMaxLength(15);
            
            builder.Property(g => g.Capacity)
                   .IsRequired();

            builder.HasCheckConstraint("CK_Group_Capacity", "[Capacity] > 0");

            builder.HasCheckConstraint(
                    "CK_Group_Enrolled",
                    "[CurrentEnrolled] >= 0 AND [CurrentEnrolled] <= [Capacity]"
                    );
            builder.Property(g => g.StartTime)
                   .HasColumnType("time");

            builder.Property(g => g.EndTime)
                   .HasColumnType("time");

            builder.HasCheckConstraint(
                   "CK_Group_Time",
                   "[EndTime] > [StartTime]"
                   );

            builder.Ignore(g => g.RemainingCapacity);

            //Relationships
            builder.HasOne(g => g.Teacher)
                   .WithMany(t => t.Groups)
                   .HasForeignKey(g => g.TeacherId)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(g => g.Branch)
                   .WithMany(b => b.Groups)
                   .HasForeignKey(g => g.BranchId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Grade)
                   .WithMany(gr => gr.Groups)
                   .HasForeignKey(g => g.GradeId)
                   .OnDelete(DeleteBehavior.Restrict);
            
            //for increase search time
            builder.HasIndex(g => g.TeacherId);
            builder.HasIndex(g => g.GradeId);
        }
    }
}

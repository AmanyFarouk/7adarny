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
    public class BranchConfigurations : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(b => b.Address)
                   .HasMaxLength(100);

            //Relationships
            builder.HasOne(b => b.Teacher)
                   .WithMany(t => t.Branches)
                   .HasForeignKey(b => b.TeacherId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(b => b.Groups)
                   .WithOne(g => g.Branch)
                   .HasForeignKey(g => g.BranchId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

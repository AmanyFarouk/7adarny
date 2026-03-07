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
    public class NotificationConfigurations : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Message)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(n => n.ActionUrl)
                   .HasMaxLength(200);

            //Relationships
            builder.HasOne(n => n.Teacher)
                   .WithMany(t => t.Notifications)
                   .HasForeignKey(n => n.TeacherId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Index لتسريع جلب إشعارات مدرس معين
            builder.HasIndex(n => new { n.TeacherId, n.IsRead });
        }
    }
}

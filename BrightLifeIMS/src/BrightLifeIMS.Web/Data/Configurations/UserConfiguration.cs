// src/BrightLifeIMS.Web/Data/Configurations/UserConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BrightLifeIMS.Web.Models.Entities;

namespace BrightLifeIMS.Web.Data
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Role)
                .HasConversion<int>();

            builder.Property(u => u.PreferredLanguage)
                .HasMaxLength(10)
                .HasDefaultValue("en-US");

            builder.HasIndex(u => u.Email);
            builder.HasIndex(u => u.UserName);

            // Configure relationships for entities that exist in DbContext
            builder.HasMany(u => u.CreatedInventories)
                .WithOne()
                .HasForeignKey(i => i.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.CreatedItems)
                .WithOne()
                .HasForeignKey(i => i.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

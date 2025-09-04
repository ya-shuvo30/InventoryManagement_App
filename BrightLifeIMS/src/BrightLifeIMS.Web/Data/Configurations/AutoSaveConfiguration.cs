// src/Bright            builder.Property(a => a.AutoSaveData);feIMS.Web/Data/Configurations/AutoSaveConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BrightLifeIMS.Web.Models.Entities;

namespace BrightLifeIMS.Web.Data
{
    public class AutoSaveConfiguration : IEntityTypeConfiguration<AutoSave>
    {
        public void Configure(EntityTypeBuilder<AutoSave> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.SaveData)
                .HasColumnType("jsonb");

            builder.HasOne(a => a.Inventory)
                .WithMany(i => i.AutoSaves)
                .HasForeignKey(a => a.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => new { a.InventoryId, a.CreatedAt });
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BrightLifeIMS.Web.Models.Entities;

namespace BrightLifeIMS.Web.Data
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(i => i.Id);
            
            builder.Property(i => i.Title).IsRequired().HasMaxLength(255);
            
            // Configure auto-save data as TEXT for SQLite JSON storage
            builder.Property(i => i.AutoSaveData)
                .HasColumnType("TEXT");
            
            // Configure relationships
            builder.HasMany(i => i.Items)
                .WithOne(item => item.Inventory)
                .HasForeignKey(item => item.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

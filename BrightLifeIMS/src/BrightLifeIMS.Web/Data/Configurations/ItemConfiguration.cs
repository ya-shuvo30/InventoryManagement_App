using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BrightLifeIMS.Web.Models.Entities;

namespace BrightLifeIMS.Web.Data
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(i => i.Id);
            
            // Configure JSON field for SQLite (use TEXT column type)
            builder.Property(i => i.CustomFieldsJson)
                .HasColumnType("TEXT")
                .HasColumnName("CustomFields");
            
            // Configure relationship with Inventory
            builder.HasOne(i => i.Inventory)
                .WithMany(inv => inv.Items)
                .HasForeignKey(i => i.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

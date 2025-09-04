// src/BrightLifeIMS.Web/Data/Configurations/ItemLikeConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BrightLifeIMS.Web.Models.Entities;

namespace BrightLifeIMS.Web.Data
{
    public class ItemLikeConfiguration : IEntityTypeConfiguration<ItemLike>
    {
        public void Configure(EntityTypeBuilder<ItemLike> builder)
        {
            builder.HasKey(il => il.Id);

            builder.HasOne(il => il.Item)
                .WithMany(i => i.Likes)
                .HasForeignKey(il => il.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(il => il.User)
                .WithMany(u => u.ItemLikes)
                .HasForeignKey(il => il.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(il => new { il.ItemId, il.UserId })
                .IsUnique()
                .HasDatabaseName("idx_unique_like_per_user_item");
        }
    }
}
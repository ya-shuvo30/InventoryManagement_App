// src/BrightLifeIMS.Web/Data/Configurations/CategoryConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BrightLifeIMS.Web.Models.Entities;

namespace BrightLifeIMS.Web.Data
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(c => c.Name)
                .IsUnique();

            builder.HasIndex(c => c.DisplayOrder);

            // Temporarily disable seed data to avoid dynamic value issues
            // Will be re-enabled in Phase 2
            /*
            builder.HasData(
                new Category { Id = 1, Name = "Physical Goods", NameBn = "শারীরিক পণ্য", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Membership Services", NameBn = "সদস্যপদ সেবা", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Partner Claims", NameBn = "অংশীদার দাবি", DisplayOrder = 3 }
            );
            */
        }
    }
}


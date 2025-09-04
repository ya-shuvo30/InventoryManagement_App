// src/BrightLifeIMS.Web/Data/AppDbContext.cs
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BrightLifeIMS.Web.Models.Entities;
using System.Text.Json;

namespace BrightLifeIMS.Web.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<InventoryTag> InventoryTags { get; set; }
        // public DbSet<ItemLike> ItemLikes { get; set; }
        // public DbSet<Comment> Comments { get; set; }
        // public DbSet<AutoSave> AutoSaves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply basic configurations only
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryTagConfiguration());
            // Other advanced configurations commented out temporarily
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Inventory || e.Entity is Item)
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Entity is Inventory inventory)
                {
                    inventory.UpdatedAt = DateTime.UtcNow;
                    if (entry.State == EntityState.Added)
                    {
                        inventory.CreatedAt = DateTime.UtcNow;
                    }
                }
                else if (entry.Entity is Item item)
                {
                    item.UpdatedAt = DateTime.UtcNow;
                    if (entry.State == EntityState.Added)
                    {
                        item.CreatedAt = DateTime.UtcNow;
                    }
                }
            }
        }
    }
}

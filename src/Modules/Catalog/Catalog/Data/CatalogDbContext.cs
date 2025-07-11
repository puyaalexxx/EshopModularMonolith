﻿using Catalog.Products.Models;


namespace Catalog.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("catalog");

            builder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);

            base.OnModelCreating(builder);
        }
    }
}

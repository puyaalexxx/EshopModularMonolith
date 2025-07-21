


using Basket.Basket.Models;

namespace Catalog.Data
{
    public class BasketDbContext : DbContext
    {
        public BasketDbContext(DbContextOptions<BasketDbContext> options) : base(options)
        {
        }

        public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
        public DbSet<ShoppingCartItem> ShoppingCartItems => Set<ShoppingCartItem>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("basket");

            builder.ApplyConfigurationsFromAssembly(typeof(BasketDbContext).Assembly);

            base.OnModelCreating(builder);
        }
    }
}

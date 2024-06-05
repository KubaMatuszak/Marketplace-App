using Marketplace_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace_App.Data
{
    public class MarketplaceContext : DbContext
    {
        public MarketplaceContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> products { get; set; }
    }
}

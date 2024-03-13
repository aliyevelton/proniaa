using Microsoft.EntityFrameworkCore;
using ProniaAgain;
using ProniaAgain.Models;

namespace ProniaAgain.Contexts
{
    public class ProniaDbContext : DbContext
    {
        public ProniaDbContext(DbContextOptions<ProniaDbContext> options) : base(options)
        {
        }
        public DbSet<Shipping> Shippings { get; set; } = null!;
        public DbSet<Slider> Sliders { get; set; } = null!;
    }
}

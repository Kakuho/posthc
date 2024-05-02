using Microsoft.EntityFrameworkCore;

namespace Phc.Data
{
    public class PhcContext : DbContext
    {
        public PhcContext(DbContextOptions<PhcContext> options)
            : base(options)
        {
        }

        public DbSet<Band> Bands { get; set; }
    }
}

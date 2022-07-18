using Microsoft.EntityFrameworkCore;

namespace Swm.Api.Business.Data
{
#nullable disable
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Entities.User> Users { get; set; }
    }
#nullable enable
}

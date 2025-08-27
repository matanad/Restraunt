using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class TableContext : DbContext
    {
        public TableContext(DbContextOptions<TableContext> options) : base(options)
        {
        }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }

}

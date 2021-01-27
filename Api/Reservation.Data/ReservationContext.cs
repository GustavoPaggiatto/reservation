using Microsoft.EntityFrameworkCore;
using Reservation.Domains.Entities;

namespace Reservation.Data
{
    /// <summary>
    /// Implementation of DbContext for EF Core requirements (see https://docs.microsoft.com/en-US/ef/core/ for complete references).
    /// </summary>
    internal class ReservationContext : DbContext
    {
        private readonly string _strConnection;
        public DbSet<Reserve> Reserve { get; set; }
        public DbSet<ContactType> ContactType { get; set; }
        public DbSet<Contact> Contact { get; set; }
        
        public ReservationContext(string strConnection)
        {
            this._strConnection = strConnection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._strConnection);
        }
    }
}

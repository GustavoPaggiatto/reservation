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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContactType>(ct =>
            {
                ct.ToTable("ContactType");
                ct.HasKey("Id");
                ct.Property("Id").HasColumnName("id").ValueGeneratedOnAdd();
                ct.Property("Description").HasColumnName("description").IsRequired().HasMaxLength(200);
                ct.HasData(
                    new ContactType() { Id = 1, Description = "Phisical" },
                    new ContactType() { Id = 2, Description = "Company" });
            });

            modelBuilder.Entity<Contact>(c =>
            {
                c.ToTable("Contact");
                c.HasKey("Id");
                c.Property("Id").HasColumnName("id").ValueGeneratedOnAdd();
                c.Property("Name").HasColumnName("name").IsRequired().HasMaxLength(200);
                c.Property("BirthDate").HasColumnName("birthDate").IsRequired();
                c.Property("ContactTypeId").HasColumnName("contactTypeId").IsRequired();
                c.Property("Phone").HasColumnName("phone");
                c.Property("Logo").HasColumnName("logo");
                c.HasOne(co => co.ContactType).WithMany(ct => ct.Contacts);
            });

            modelBuilder.Entity<Reserve>(r =>
            {
                r.ToTable("Reserve");
                r.HasKey("Id");
                r.Property("Id").HasColumnName("Id").ValueGeneratedOnAdd();
                r.Property("Schedule").HasColumnName("schedule").IsRequired();
                r.Property("Description").HasColumnName("description").IsRequired();
                r.Property("Ranking").HasColumnName("ranking").HasDefaultValue(0).IsRequired();
                r.Property("Favorite").HasColumnName("favorite").HasDefaultValue(0).IsRequired();
                r.HasOne(c => c.Contact).WithMany(cr => cr.Reservs);
            });

            //modelBuilder.UseSqlServerIdentityColumns();
            modelBuilder.HasDefaultSchema("dbo");
        }
    }
}

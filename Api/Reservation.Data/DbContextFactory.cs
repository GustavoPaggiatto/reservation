using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Data
{
    class DbContextFactory : IDesignTimeDbContextFactory<ReservationContext>
    {
        public ReservationContext CreateDbContext(string[] args)
        {
            return new ReservationContext("Data Source=localhost\\Projects;Initial Catalog=Reservation;user id=sa;password=G31h07cp1994*;");
        }
    }
}

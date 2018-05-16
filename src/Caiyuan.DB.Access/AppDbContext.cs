
using Caiyuan.DB.Models;
using Caiyuan.DB.Models.MapConfigration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caiyuan.DB.Access
{
    public class AppDbContext : IdentityDbContext<User>
    {

        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Doctor> Doctors
        {
            get;
            set;
        }
        public DbSet<Reservation> Reservations
        {
            get;
            set;
        }
 
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Clinic>(typeBuilder => { ClinicMap.Map(typeBuilder); });
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}

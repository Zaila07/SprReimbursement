
using Microsoft.EntityFrameworkCore;
using SprEmployeeReimbursement.DataAccess.Models;
using System.Reflection;

namespace SprEmployeeReimbursement.DataAccess.SprDbContext
{
    public class SprReimbursementDbContext : DbContext
    {
        public SprReimbursementDbContext(DbContextOptions<SprReimbursementDbContext> options) : base(options)
        {
        }

        public DbSet<ReimbursementModel> ReimbursementModels { get; set; }
        public DbSet<SprEmployee> SprEmployees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReimbursementModel>(entity =>
            {
               entity.Property(e => e.MonthlyTotal)
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.IsApproved)
                    .IsRequired();

                entity.Property(e => e.ResponseMessage)
                    .HasMaxLength(255);

            });
          
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace SprEmployeeReimbursement.Models
{
    public class SprReimbursementDbContext: DbContext
    {
        public SprReimbursementDbContext(DbContextOptions<SprReimbursementDbContext>option) :base (option)
        {
            
        }

        public DbSet<ReimbursementModel> ReimbursementModels { get; set; }
        public DbSet<SprEmployee> SprEmployees { get; set;}

    }
}

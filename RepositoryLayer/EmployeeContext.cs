using Microsoft.EntityFrameworkCore;
using ModelLayer;
namespace RepositoryLayer
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<EmployeeDetail> employee { get; set; }
        public DbSet<AccountDetails> accountDetails { get; set; }
        public DbSet<Designation> designations { get; set; }

    }
}
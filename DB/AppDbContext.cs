using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using OSDashboardBA.Models;

namespace OSDashboardBA.DB
{
    // public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    // public class AppDbContext : DbContext
    public class AppDbContext : DbContext
    {
        // constructor to link appDbContext and DbContext or IdentityDbContext made with EF
        // pass options from builder services using postgreSQL server to parent class "dbContext or IdentityDbContext" 
        // when initializing new instances from class 
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {

        }

        // props // add classes as tables in db 
        public DbSet<User>? Users { get; set; }   // useres
        public DbSet<Dashboard>? Dashboards { get; set; }
        public DbSet<Layer>? Layers { get; set; }




    }
}

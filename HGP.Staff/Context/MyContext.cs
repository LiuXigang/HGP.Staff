using HGP.Staff.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace HGP.Staff.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasKey(x => x.Id);
        }

        public DbSet<Department> Departments { get; set; }
    }
}

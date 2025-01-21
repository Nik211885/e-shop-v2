using Infrastructure.Services.SlaveDbSelector;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class EfApplicationDbContext(ISlaveDbSelector slaveDbSelector) : DbContext
    {
        private readonly ISlaveDbSelector _slaveDbSelector = slaveDbSelector;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
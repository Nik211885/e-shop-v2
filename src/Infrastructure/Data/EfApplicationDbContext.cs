using Core.Entities;
using Core.Entities.Test;
using Infrastructure.Services.SlaveDbSelector;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Infrastructure.Data
{
    public class EfApplicationDbContext(IPublisher publisher, DbContextOptions<EfApplicationDbContext> options) 
        : DbContext(options)
    {
        public DbSet<EntityBoundTest> TestCase {get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var eventBases = ChangeTracker.Entries<BaseEntity>()
                .Where(x => x.Entity.Events is not null && x.Entity.Events.Any())
                .Select(x => x.Entity.Events).ToList();
            var result = await base.SaveChangesAsync(cancellationToken);
            foreach (var events in eventBases)
            {
                foreach (var e in events ?? [])
                {
                    await publisher.Publish(e,cancellationToken);
                }       
            }
            return result;
        }
    }
}
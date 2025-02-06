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
            var eventBases = from entity in ChangeTracker.Entries<BaseEntity>()
                where entity.Entity.Events is not null &&
                      entity.Entity.Events.Any()
                select entity;
            var result = await base.SaveChangesAsync(cancellationToken);
            foreach (var e in eventBases)
            {
                await publisher.Publish(e, cancellationToken);        
            }
            return result;
        }
    }
}
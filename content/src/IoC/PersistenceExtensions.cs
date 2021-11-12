namespace Linn.Template.IoC
{
    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;
    using Linn.Template.Domain.LinnApps;
    using Linn.Template.Domain.LinnApps.Consignments;
    using Linn.Template.Persistence.LinnApps;
    using Linn.Template.Persistence.LinnApps.Repositories;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            return services.AddScoped<ServiceDbContext>()
                .AddTransient<DbContext>(a => a.GetService<ServiceDbContext>())
                .AddTransient<ITransactionManager, TransactionManager>()
                .AddTransient<IRepository<Hub, int>, EntityFrameworkRepository<Hub, int>>(r => new EntityFrameworkRepository<Hub, int>(r.GetService<ServiceDbContext>()?.Hubs))
                .AddTransient<IRepository<Thing, int>, ThingRepository>();
        }
    }
}

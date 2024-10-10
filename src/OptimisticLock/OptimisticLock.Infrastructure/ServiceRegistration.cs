using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OptimisticLock.Infrastructure.Persistence;

namespace OptimisticLock.Infrastructure;

public static class ServiceRegistration
{
    public static void RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OptimisticLockDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("MsSql"));
        });
    }
}

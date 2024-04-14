using DataAccess.Db;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddDbContext<PjatkDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("Pjatk"));
                })
                .AddScoped<IPjatkRepository, PjatkRepository>();
    }
}
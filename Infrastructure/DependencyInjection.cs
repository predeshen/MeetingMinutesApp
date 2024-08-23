using MeetingMinutesApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MeetingMinutesApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ResolutionActionContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}

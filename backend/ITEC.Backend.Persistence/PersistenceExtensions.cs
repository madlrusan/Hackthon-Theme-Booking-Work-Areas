using ITEC.Backend.Persistence.Database;
using ITEC.Backend.Persistence.Repositories;
using ITEC.Backend.Persistence.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ITEC.Backend.Persistence
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly("ITEC.Backend.Persistence"))
            );
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IOfficeRepository, OfficeRepository>();
            services.AddScoped<IDeskReservationRepository, DeskReservationRepository>();
            services.AddScoped<IFloorRepository, FloorRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }

    }
}

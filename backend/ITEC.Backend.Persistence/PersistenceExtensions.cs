using ITEC.Backend.Persistence.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }

    }
}

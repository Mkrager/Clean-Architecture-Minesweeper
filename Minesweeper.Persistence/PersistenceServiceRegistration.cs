using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minesweeper.Application.Contracts.Persistance;
using Minesweeper.Persistence.Repositories;

namespace Minesweeper.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this
            IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MinesweeperDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString
            ("MinesweeperConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            return services;
        }
    }
}

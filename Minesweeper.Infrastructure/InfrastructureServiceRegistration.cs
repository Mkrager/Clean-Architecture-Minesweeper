using Microsoft.Extensions.DependencyInjection;
using Minesweeper.Application.Contracts.Infrastructure;
using Minesweeper.Infrastructure.Services;

namespace Minesweeper.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<IMinesweeperService, MinesweeperService>();
            services.AddTransient<IGameEngine, GameEngine>();
            services.AddTransient<IMinesweeperSolverService, MinesweeperSolver>();

            services.AddTransient<IMoveStrategy, SimpleLogicStrategy>();
            services.AddTransient<IMoveStrategy, SubsetLogicStrategy>();
            services.AddTransient<IMoveStrategy, GuessStrategy>();

            services.AddMemoryCache();
            return services;
        }
    }
}

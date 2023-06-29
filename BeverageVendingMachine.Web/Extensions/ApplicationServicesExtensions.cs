using BeverageVendingMachine.Core.Common;
using BeverageVendingMachine.Core.Handlers;
using BeverageVendingMachine.Core.Interfaces.Handlers;
using BeverageVendingMachine.Core.Interfaces.Repositories;
using BeverageVendingMachine.Core.Interfaces.Services;
using BeverageVendingMachine.Core.Services;
using BeverageVendingMachine.Infrastructure.Data;
using BeverageVendingMachine.Infrastructure.Repositories;
using BeverageVendingMachine.Infrastructure.SeedData;
using Microsoft.Extensions.DependencyInjection;

namespace BeverageVendingMachine.Web.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<BeverageVendingMachineContext, BeverageVendingMachineContext>();
            services.AddScoped<BeverageVendingMachineContextSeed, BeverageVendingMachineContextSeed>();
            services.AddSingleton<ICoinDenominationRepository, CoinDenominationRepository>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IStorageService, StorageService>();
            services.AddSingleton<ITerminalService, TerminalService>();
            services.AddSingleton<IAdminTerminalService, AdminTerminalService>();
            services.AddSingleton<IUserTerminalService, UserTerminalService>();
            services.AddSingleton<IFileHandler, FileHandler>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}

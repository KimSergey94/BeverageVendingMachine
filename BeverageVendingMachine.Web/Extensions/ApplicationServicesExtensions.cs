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
    /// <summary>
    /// Extension class to configure dependency injection for services
    /// </summary>
    public static class ApplicationServicesExtensions
    {
        /// <summary>
        /// Configures dependency injection and adds instances to the service collection that called the extension
        /// </summary>
        /// <param name="services">Service collection that calls this extension</param>
        /// <returns>The service collection that called this extension with added services</returns>
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

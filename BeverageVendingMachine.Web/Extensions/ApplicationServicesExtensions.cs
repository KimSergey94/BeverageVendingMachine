using BeverageVendingMachine.Core.Common;
using BeverageVendingMachine.Core.Interfaces.Repositories;
using BeverageVendingMachine.Core.Interfaces.Services;
using BeverageVendingMachine.Core.Services;
using BeverageVendingMachine.Infrastructure.Data;
using BeverageVendingMachine.Infrastructure.Repositories;
using BeverageVendingMachine.Infrastructure.SeedData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BeverageVendingMachine.Web.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<BeverageVendingMachineContext, BeverageVendingMachineContext>();
            services.AddScoped<BeverageVendingMachineContextSeed, BeverageVendingMachineContextSeed>();
            services.AddScoped<ICoinDenominationRepository, CoinDenominationRepository>();
            services.AddScoped<ITerminalService, TerminalService>();
            services.AddScoped<IAdminTerminalService, AdminTerminalService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}

using BeverageVendingMachine.Core.Common;
using BeverageVendingMachine.Core.Interfaces.Repositories;
using BeverageVendingMachine.Core.Interfaces.Services;
using BeverageVendingMachine.Core.Services;
using BeverageVendingMachine.Infrastructure.Data;
using BeverageVendingMachine.Infrastructure.Repositories;
using BeverageVendingMachine.Infrastructure.SeedData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

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
            //services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IStorageService>(x => new StorageService(x.GetRequiredService<IUnitOfWork>()));//new StorageService(x.GetRequiredService<BeverageVendingMachineContext>().CoinDenominations.ToList(), x.GetRequiredService<BeverageVendingMachineContext>().StorageItems.ToList()));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}

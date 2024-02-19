using GabrielesProject.AdformExam.Application.Interfaces;
using GabrielesProject.AdformExam.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GabrielesProject.AdformExam.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(OrdersService));
        services.AddTransient<IExternalUserService, ExternalUserService>();
        services.AddTransient<IItemsService, ItemsService>();
        services.AddTransient<IOrdersService, OrdersService>();
    }
}

using GabrielesProject.AdformExam.Application.Interfaces;
using GabrielesProject.AdformExam.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GabrielesProject.AdformExam.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IExternalUserService, ExternalUserService>();
    }

}

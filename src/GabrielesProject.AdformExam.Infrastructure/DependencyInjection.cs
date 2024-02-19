using GabrielesProject.AdformExam.Application.Interfaces;
using GabrielesProject.AdformExam.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;

namespace GabrielesProject.AdformExam.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, string? dbConnectionString)
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(dbConnectionString));
        services.AddTransient<IItemsRepository, ItemsRepository>();
        services.AddTransient<IOrdersRepository, OrdersRepository>();
    }
}

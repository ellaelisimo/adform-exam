using Dapper;
using GabrielesProject.AdformExam.Application.Interfaces;
using GabrielesProject.AdformExam.Domain.Entities;
using System.Data;

namespace GabrielesProject.AdformExam.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IDbConnection _connection;

    public OrderRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public Task<int> AddOrder(Order order)
    {
        return _connection.QueryFirstOrDefaultAsync<int>("INSERT INTO orders (status, user_id) VALUES (@status, @userId) RETURNING id", order);
    }

    public Task<int> DeleteOrderAsync(int id, string orderStatus)
    {
        return _connection.ExecuteAsync("UPDATE orders SET status=@orderStatus WHERE id=@id", new { id, orderStatus });
    }

    public Task<Order?> GetOrderAsync(int id)
    {
        return _connection.QueryFirstOrDefaultAsync<Order>("SELECT * FROM orders WHERE id=@id", new { id });
    }

    public Task<IEnumerable<Order>> GetOrdersAsync()
    {
        return _connection.QueryAsync<Order>("SELECT * FROM orders");
    }

    public Task<int> UpdateOrder(int id, string orderStatus)
    {
        return _connection.ExecuteAsync("UPDATE orders SET status=@orderStatus WHERE id=@id", new { id, orderStatus });
    }

    public async Task<IEnumerable<Order>> GetUnpaidOrdersOlderThanTwoHoursAsync()
    {
        var twoHoursAgo = DateTime.UtcNow.AddHours(-2);
        var sql = "SELECT * FROM orders WHERE status = 'not paid' AND created_at < @twoHoursAgo";
        return await _connection.QueryAsync<Order>(sql, new { twoHoursAgo });
    }
}

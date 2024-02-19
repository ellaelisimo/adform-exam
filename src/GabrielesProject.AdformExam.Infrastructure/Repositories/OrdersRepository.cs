using Dapper;
using GabrielesProject.AdformExam.Application.Interfaces;
using GabrielesProject.AdformExam.Domain.Entities;
using System.Data;

namespace GabrielesProject.AdformExam.Infrastructure.Repositories;

public class OrdersRepository : IOrdersRepository
{
    private readonly IDbConnection _connection;

    public OrdersRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<int> AddOrder(Order order)
    {
        return await _connection.QueryFirstOrDefaultAsync<int>("INSERT INTO orders (status, user_id, item_id) VALUES (@status, @userId, @itemId) RETURNING id", new { status = order.Status, userId = order.UserId, itemId = order.ItemId });
    }

    public async Task<int> DeleteOrderAsync(int id)
    {
        return await _connection.ExecuteAsync("DELETE FROM order WHERE id=@id", new { id });
    }

    public async Task<Order?> GetOrderAsync(int id)
    {
        return await _connection.QueryFirstOrDefaultAsync<Order>("SELECT * FROM orders WHERE id=@id", new { id });
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        return await _connection.QueryAsync<Order>("SELECT * FROM orders");
    }

    public async Task<int> UpdateOrder(int id, string orderStatus)
    {
        return await _connection.ExecuteAsync("UPDATE orders SET status=@orderStatus WHERE id=@id", new { id, orderStatus });
    }

    public async Task<IEnumerable<Order>> GetUnpaidOrdersOlderThanTwoHoursAsync()
    {
        var status = "completed";
        var twoHoursAgo = DateTime.UtcNow.AddHours(-2);
        var sql = "SELECT * FROM orders WHERE status <> @status AND created_at > @twoHoursAgo";
        return await _connection.QueryAsync<Order>(sql, new { twoHoursAgo });
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
    {
        var query = "SELECT * FROM orders WHERE user_id = @UserId";
        var parameters = new { UserId = userId };

        var orders = await _connection.QueryAsync<Order>(query, parameters);

        return orders;
    }
}

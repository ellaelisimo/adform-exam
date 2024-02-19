using Dapper;
using GabrielesProject.AdformExam.Application.Interfaces;
using System.Data;

namespace GabrielesProject.AdformExam.Infrastructure.Repositories;

public class OrdersItemsRepository : IOrdersItemsRepository
{
    private readonly IDbConnection _connection;

    public OrdersItemsRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public Task<int> AddOrderItem(int orderId, int itemId)
    {
        return _connection.ExecuteAsync("INSERT INTO orders_items (order_id, item_id) VALUES (@orderId, @itemId)", new { orderId, itemId });
    }
}

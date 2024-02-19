using Dapper;
using GabrielesProject.AdformExam.Application.Interfaces;
using GabrielesProject.AdformExam.Domain.Entities;
using System.Data;

namespace GabrielesProject.AdformExam.Infrastructure.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly IDbConnection _connection;

    public ItemRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public Task<int> AddItemAsync(Item item)
    {
        return _connection.QueryFirstOrDefaultAsync<int>("INSERT INTO items (name) VALUES (@name) RETURNING id", item);
    }

    public Task<IEnumerable<Item>> GetItemsAsync()
    {
        return _connection.QueryAsync<Item>("SELECT * FROM items");
    }

    public Task<Item?> GetItemAsync(int id)
    {
        return _connection.QueryFirstOrDefaultAsync<Item>("SELECT * FROM items WHERE id=@id", new { id });
    }
}

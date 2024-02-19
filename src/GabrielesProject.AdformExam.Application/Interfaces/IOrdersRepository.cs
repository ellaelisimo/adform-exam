using GabrielesProject.AdformExam.Domain.Entities;

namespace GabrielesProject.AdformExam.Application.Interfaces;

public interface IOrdersRepository
{
    public Task<IEnumerable<Order>> GetOrdersAsync();

    public Task<Order?> GetOrderAsync(int id);

    public Task<int> AddOrder(Order order);

    public Task<int> DeleteOrderAsync(int id, string orderStatus);

    public Task<int> UpdateOrder(int id, string orderStatus);

    public Task<IEnumerable<Order>> GetUnpaidOrdersOlderThanTwoHoursAsync();

    public Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
}

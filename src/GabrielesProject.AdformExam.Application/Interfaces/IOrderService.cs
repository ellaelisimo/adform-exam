using GabrielesProject.AdformExam.Domain.Entities;

namespace GabrielesProject.AdformExam.Application.Interfaces;

public interface IOrderService
{
    public Task<IEnumerable<Order>> GetOrdersAsync();

    public Task<Order?> GetOrderAsync(int id);

    public Task<int> AddOrder(Order order);

    // public Task<int> DeleteOrderAsync(int id, string orderStatus);
    public Task<bool> DeleteAsNotPaidAfterTwoHours();

    public Task<int> UpdateOrder(int id, string orderStatus);
}

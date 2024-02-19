using GabrielesProject.AdformExam.Application.DTOs;
using GabrielesProject.AdformExam.Domain.Entities;

namespace GabrielesProject.AdformExam.Application.Interfaces;

public interface IOrdersService
{
    public Task<IEnumerable<OrderDTO>> GetOrdersAsync();

    public Task<OrderDTO?> GetOrderAsync(int id);

    public Task<int> AddOrder(OrderDTO order);

    public Task<bool> DeleteAsNotPaidAfterTwoHours();

    public Task<int> UpdateOrder(int id, string orderStatus);

    public Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(int userId);
}

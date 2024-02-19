using GabrielesProject.AdformExam.Application.DTOs;
using GabrielesProject.AdformExam.Domain.Entities;

namespace GabrielesProject.AdformExam.Application.Interfaces;

public interface IOrdersService
{
    public Task<IEnumerable<OrderDTO>> GetOrdersAsync();

    public Task<OrderDTO?> GetOrderAsync(int id);

    public Task<OrderDTO> AddOrder(NewOrderDTO order);

    public Task<bool> DeleteAsNotPaidAfterTwoHours();

    public Task<string> UpdateOrderStatus(int id, string orderStatus);

    public Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(int userId);
}

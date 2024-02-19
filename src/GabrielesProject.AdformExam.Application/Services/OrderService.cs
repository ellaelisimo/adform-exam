using GabrielesProject.AdformExam.Application.Interfaces;
using GabrielesProject.AdformExam.Domain.Entities;
using GabrielesProject.AdformExam.Domain.Exceptions;

namespace GabrielesProject.AdformExam.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IExternalUserService _userService;

    public OrderService(IOrderRepository orderRepository, IExternalUserService userService)
    {
        _orderRepository = orderRepository;
        _userService = userService;
    }

    public async Task<int> AddOrder(Order order)
    {
        ExternalUser user = await _userService.GetUserAsync(order.UserId);
        if(user == null)
        {
            throw new NotFoundException("User doesn't exist");
        }

        order.UserId = user.Id;
        order.Status = "created";
        var orderId = await _orderRepository.AddOrder(order);
        return orderId;
    }

    public Task<Order?> GetOrderAsync(int id)
    {
        var order = _orderRepository.GetOrderAsync(id);
        if (order == null)
        {
            throw new NotFoundException($"Order with {id} doesn't exist");
        }
        return order;
    }

    public Task<IEnumerable<Order>> GetOrdersAsync()
    {
        return _orderRepository.GetOrdersAsync();
    }

    public Task<int> UpdateOrder(int id, string orderStatus)
    {
        return _orderRepository.UpdateOrder(id, orderStatus);
    }

    public async Task<bool> DeleteAsNotPaidAfterTwoHours()
    {
        var ordersToMarkAsNotPaid = await _orderRepository.GetUnpaidOrdersOlderThanTwoHoursAsync();
        foreach(var order in ordersToMarkAsNotPaid)
        {
            await _orderRepository.UpdateOrder(order.Id, "not paid");
        }
        return true;
    }
}

using GabrielesProject.AdformExam.Application.DTOs;
using GabrielesProject.AdformExam.Application.Interfaces;
using GabrielesProject.AdformExam.Domain.Entities;
using GabrielesProject.AdformExam.Domain.Exceptions;

namespace GabrielesProject.AdformExam.Application.Services;

public class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IExternalUserService _usersService;
    private readonly IItemsService _itemsService;

    public OrdersService(IOrdersRepository ordersRepository, IExternalUserService usersService, IItemsService itemsService)
    {
        _ordersRepository = ordersRepository;
        _usersService = usersService;
        _itemsService = itemsService;
    }

    public async Task<int> AddOrder(OrderDTO order)
    {
        ExternalUser user = await _usersService.GetUserAsync(order.UserId);
        if (user == null)
        {
            throw new NotFoundException("User doesn't exist");
        }

        order.UserId = user.Id;
        order.Status = "created";

        Order orderEntity = ConvertToOrderEntity(order);

        var orderId = await _ordersRepository.AddOrder(orderEntity);
        return orderId;
    }

    public async Task<OrderDTO?> GetOrderAsync(int id)
    {
        var orderEntity = await _ordersRepository.GetOrderAsync(id);
        if (orderEntity == null)
        {
            throw new NotFoundException($"Order with {id} doesn't exist");
        }

        OrderDTO orderDTO = ConvertToOrderDTO(orderEntity);
        return orderDTO;
    }

    public async Task<IEnumerable<OrderDTO>> GetOrdersAsync()
    {
        var orderEntities = await _ordersRepository.GetOrdersAsync();

        var orderDTOs = orderEntities.Select(ConvertToOrderDTO);

        return orderDTOs;
    }

    public Task<int> UpdateOrder(int id, string orderStatus)
    {
        return _ordersRepository.UpdateOrder(id, orderStatus);
    }

    public async Task<bool> DeleteAsNotPaidAfterTwoHours()
    {
        var ordersToMarkAsNotPaid = await _ordersRepository.GetUnpaidOrdersOlderThanTwoHoursAsync();
        foreach(var order in ordersToMarkAsNotPaid)
        {
            await _ordersRepository.UpdateOrder(order.Id, "not paid");
        }
        return true;
    }

    public async Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(int userId)
    {
        var orderEntities = await _ordersRepository.GetOrdersByUserIdAsync(userId);
        var orderDTOs = orderEntities.Select(ConvertToOrderDTO);
        return orderDTOs;
    }

    private Order ConvertToOrderEntity(OrderDTO orderDTO)
    {
        return new Order
        {
            Id = orderDTO.Id,
            UserId = orderDTO.UserId,
        };
    }

    private OrderDTO ConvertToOrderDTO(Order order)
    {
        return new OrderDTO
        {
            Id = order.Id,
            UserId = order.UserId,
        };
    }
}

using GabrielesProject.AdformExam.Application.DTOs;
using GabrielesProject.AdformExam.Application.Interfaces;
using GabrielesProject.AdformExam.Domain.Entities;
using GabrielesProject.AdformExam.Domain.Exceptions;

namespace GabrielesProject.AdformExam.Application.Services;

public class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IExternalUserService _usersService;

    public OrdersService(IOrdersRepository ordersRepository, IExternalUserService usersService)
    {
        _ordersRepository = ordersRepository;
        _usersService = usersService;
    }

    public async Task<OrderDTO> AddOrder(NewOrderDTO order)
    {
        ExternalUser user = await _usersService.GetUserAsync(order.UserId);
        if (user == null)
        {
            throw new NotFoundException("User doesn't exist");
        }

        Order orderEntity = ConvertToOrderEntity(order);

        var orderId = await _ordersRepository.AddOrder(orderEntity);
        return await GetOrderAsync(orderId) ?? throw new NotCreatedException("Couldn't create order");
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

    public async Task<string> UpdateOrderStatus(int id, string orderStatus)
    {
        var orderUpdated = await _ordersRepository.UpdateOrder(id, orderStatus);
        if(orderUpdated == 0)
        {
            throw new StatusException("Status wasn't updated");
        }
        var order = await GetOrderAsync(id) ?? throw new NotFoundException("Order not found");
        return order.Status ?? throw new StatusException("Status wasn't updated"); 
    }

    public async Task<bool> DeleteAsNotPaidAfterTwoHours()
    {
        var ordersToMarkAsNotPaid = await _ordersRepository.GetUnpaidOrdersOlderThanTwoHoursAsync();
        foreach(var order in ordersToMarkAsNotPaid)
        {
            await _ordersRepository.DeleteOrderAsync(order.Id);
        }
        return true;
    }

    public async Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(int userId)
    {
        var orderEntities = await _ordersRepository.GetOrdersByUserIdAsync(userId);
        var orderDTOs = orderEntities.Select(ConvertToOrderDTO);
        return orderDTOs;
    }

    private Order ConvertToOrderEntity(NewOrderDTO orderDTO)
    {
        return new Order
        {
            UserId = orderDTO.UserId,
            Status = "created",
            ItemId = orderDTO.ItemId
        };
    }

    private OrderDTO ConvertToOrderDTO(Order order)
    {
        return new OrderDTO
        {
            Id = order.Id,
            UserId = order.UserId,
            Status = order.Status,
            ItemId = order.ItemId,
            CreatedAt = order.CreatedAt
        };
    }
}

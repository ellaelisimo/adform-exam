using AutoFixture;
using FluentAssertions;
using GabrielesProject.AdformExam.Application.DTOs;
using GabrielesProject.AdformExam.Application.Interfaces;
using GabrielesProject.AdformExam.Application.Services;
using GabrielesProject.AdformExam.Domain.Entities;
using GabrielesProject.AdformExam.Domain.Exceptions;
using Moq;

namespace GabrielesProject.AdformExam.ServiceTests;

public class OrdersServiceTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IOrdersRepository> _mockOrdersRepository;
    private readonly Mock<IExternalUserService> _mockExternalUserService;
    private readonly OrdersService _ordersService;

    public OrdersServiceTests()
    {
        _fixture = new Fixture();
        _mockOrdersRepository = new Mock<IOrdersRepository>();
        _mockExternalUserService = new Mock<IExternalUserService>();

        _ordersService = new OrdersService(_mockOrdersRepository.Object, _mockExternalUserService.Object);
    }

    [Fact]
    public async Task AddOrder_ValidInput_ShouldReturnOrderDTO()
    {
        // Arrange
        var newOrderDTO = _fixture.Create<NewOrderDTO>();
        var externalUser = _fixture.Create<ExternalUser>();
        _mockExternalUserService.Setup(x => x.GetUserAsync(newOrderDTO.UserId)).ReturnsAsync(externalUser);
        _mockOrdersRepository.Setup(x => x.AddOrder(It.IsAny<Order>())).ReturnsAsync(1);
        _mockOrdersRepository.Setup(x => x.GetOrderAsync(It.IsAny<int>())).ReturnsAsync(new Order());

        // Act
        var result = await _ordersService.AddOrder(newOrderDTO);

        // Assert
        result.Should().NotBeNull().And.BeOfType<OrderDTO>();
    }

    [Fact]
    public async Task AddOrder_UserNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var newOrderDTO = _fixture.Create<NewOrderDTO>();
        _mockExternalUserService.Setup(x => x.GetUserAsync(newOrderDTO.UserId)).ReturnsAsync((ExternalUser)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _ordersService.AddOrder(newOrderDTO));
    }

}

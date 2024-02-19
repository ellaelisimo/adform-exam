using AutoFixture;
using AutoMapper;
using FluentAssertions;
using GabrielesProject.AdformExam.Application.DTOs;
using GabrielesProject.AdformExam.Application.Interfaces;
using GabrielesProject.AdformExam.Application.Services;
using GabrielesProject.AdformExam.Domain.Entities;
using GabrielesProject.AdformExam.Domain.Exceptions;
using Moq;

namespace GabrielesProject.AdformExam.ServiceTests;

public class ItemsServiceTests
{
        private readonly Fixture _fixture;
        private readonly Mock<IItemsRepository> _mockItemsRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ItemsService _itemsService;

        public ItemsServiceTests()
        {
            _fixture = new Fixture();
            _mockItemsRepository = new Mock<IItemsRepository>();
            _mockMapper = new Mock<IMapper>();

            _itemsService = new ItemsService(_mockItemsRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task AddItemAsync_ValidInput_ShouldReturnItemId()
        {
            // Arrange
            var newItemDTO = _fixture.Create<NewItemDTO>();
            var expectedItemId = _fixture.Create<int>();
            _mockItemsRepository.Setup(x => x.AddItemAsync(It.IsAny<Item>())).ReturnsAsync(expectedItemId);

            // Act
            var result = await _itemsService.AddItemAsync(newItemDTO);

            // Assert
            result.Should().Be(expectedItemId);
        }

        [Fact]
        public async Task GetItemAsync_ExistingItem_ShouldReturnItemDTO()
        {
            // Arrange
            var itemId = _fixture.Create<int>();
            var itemEntity = _fixture.Create<Item>();
            var itemDTO = _fixture.Create<ItemDTO>();

            _mockItemsRepository.Setup(x => x.GetItemAsync(itemId)).ReturnsAsync(itemEntity);
            _mockMapper.Setup(x => x.Map<ItemDTO>(itemEntity)).Returns(itemDTO);

            // Act
            var result = await _itemsService.GetItemAsync(itemId);

            // Assert
            result.Should().NotBeNull().And.BeOfType<ItemDTO>().Which.Should().BeEquivalentTo(itemDTO);
        }

        [Fact]
        public async Task GetItemAsync_NonExistingItem_ShouldThrowNotFoundException()
        {
            // Arrange
            var itemId = _fixture.Create<int>();
            _mockItemsRepository.Setup(x => x.GetItemAsync(itemId)).ReturnsAsync((Item)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _itemsService.GetItemAsync(itemId));
        }
 }


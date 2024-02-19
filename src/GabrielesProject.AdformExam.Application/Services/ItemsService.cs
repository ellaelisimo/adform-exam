using AutoMapper;
using GabrielesProject.AdformExam.Application.DTOs;
using GabrielesProject.AdformExam.Application.Interfaces;
using GabrielesProject.AdformExam.Domain.Entities;
using GabrielesProject.AdformExam.Domain.Exceptions;

namespace GabrielesProject.AdformExam.Application.Services;

public class ItemsService : IItemsService
{
    private readonly IItemsRepository _itemsRepository;
    private readonly IMapper _mapper;

    public ItemsService(IItemsRepository itemsRepository, IMapper mapper)
    {
        _itemsRepository = itemsRepository;
        _mapper = mapper;
    }

    public Task<int> AddItemAsync(ItemDTO item)
    {
        var itemEntity = new Item
        {
            Id = item.Id,
            Name = item.Name,
        };
        return _itemsRepository.AddItemAsync(itemEntity);
    }

    public async Task<ItemDTO?> GetItemAsync(int id)
    {
        var itemEntity = await _itemsRepository.GetItemAsync(id);

        if (itemEntity == null)
        {
            throw new NotFoundException($"Item with {id} doesn't exist");
        }

        return _mapper.Map<ItemDTO>(itemEntity);
    }

    public async Task<IEnumerable<ItemDTO>> GetItemsAsync()
    {
        var itemEntities = await _itemsRepository.GetItemsAsync();
        return itemEntities.Select(ConvertToItemDTO);
    }

    private ItemDTO ConvertToItemDTO(Item itemEntity)
    {
        return new ItemDTO
        {
            Id = itemEntity.Id,
            Name = itemEntity.Name,
        };
    }
}

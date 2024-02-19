using GabrielesProject.AdformExam.Application.Interfaces;
using GabrielesProject.AdformExam.Domain.Entities;
using GabrielesProject.AdformExam.Domain.Exceptions;

namespace GabrielesProject.AdformExam.Application.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;

    public ItemService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public Task<int> AddItemAsync(Item item)
    {
        return _itemRepository.AddItemAsync(item);
    }

    public Task<Item?> GetItemAsync(int id)
    {
        var item =_itemRepository.GetItemAsync(id);
        if(item == null)
        {
            throw new NotFoundException($"Item with {id} doesn't exists");
        }
        return item;
    }

    public async Task<IEnumerable<Item>> GetItemsAsync()
    {
        return await _itemRepository.GetItemsAsync();
    }
}

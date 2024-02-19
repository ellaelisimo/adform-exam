using GabrielesProject.AdformExam.Domain.Entities;

namespace GabrielesProject.AdformExam.Application.Interfaces;

public interface IItemRepository
{
    public Task<IEnumerable<Item>> GetItems();

    public Task<Item?> GetItem(int id);

    public Task<int> AddItemAsync(Item item);
}

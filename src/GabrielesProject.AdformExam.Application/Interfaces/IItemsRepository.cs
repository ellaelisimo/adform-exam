using GabrielesProject.AdformExam.Domain.Entities;

namespace GabrielesProject.AdformExam.Application.Interfaces;

public interface IItemsRepository
{
    public Task<IEnumerable<Item>> GetItemsAsync();

    public Task<Item?> GetItemAsync(int id);

    public Task<int> AddItemAsync(Item item);
}

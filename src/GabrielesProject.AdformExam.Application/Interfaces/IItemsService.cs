using GabrielesProject.AdformExam.Application.DTOs;
using GabrielesProject.AdformExam.Domain.Entities;

namespace GabrielesProject.AdformExam.Application.Interfaces;

public interface IItemsService
{
    public Task<IEnumerable<ItemDTO>> GetItemsAsync();

    public Task<ItemDTO?> GetItemAsync(int id);

    public Task<int> AddItemAsync(NewItemDTO item);
}

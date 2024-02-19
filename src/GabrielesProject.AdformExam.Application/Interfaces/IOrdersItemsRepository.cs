namespace GabrielesProject.AdformExam.Application.Interfaces;

public interface IOrdersItemsRepository
{
    public Task<int> AddOrderItem(int orderId, int itemId);
}

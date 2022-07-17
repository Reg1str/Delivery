using Delivery.DataAccess.Models;

namespace Delivery.Services.Repository;

public interface IRepository
{
    FormOrder? GetOrder(int id);
    List<FormOrder?>GetListOrders();
    void AddOrder(FormOrder? order);
    void UpdateOrder(FormOrder? order);
    void RemoveOrder(int id);
    Task<bool> SaveChangesAsync();
}
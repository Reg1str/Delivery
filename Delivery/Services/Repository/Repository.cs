using Delivery.DataAccess.EntityFramework;
using Delivery.DataAccess.Models;

namespace Delivery.Services.Repository;

public class Repository : IRepository
{
    private AppDbContext _ctx;
    
    public Repository(AppDbContext ctx)
    {
        _ctx = ctx;
    }
    
    public FormOrder? GetOrder(int id)
    {
        return _ctx.Orders.FirstOrDefault(o => o.Id == id);
    }

    public List<FormOrder?> GetListOrders()
    {
        return _ctx.Orders.ToList();
    }

    public void AddOrder(FormOrder? order)
    {
        _ctx.Orders.Add(order);
    }

    public void UpdateOrder(FormOrder? order)
    {
        _ctx.Orders.Update(order);
    }

    public void RemoveOrder(int id)
    {
        _ctx.Orders.Remove(GetOrder(id));
    }

    public async Task<bool> SaveChangesAsync()
    {
        if (await _ctx.SaveChangesAsync() > 0)
        {
            return true;
        }
        
        return false;
    }
}
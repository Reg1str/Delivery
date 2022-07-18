using Delivery.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

public class HomeController : Controller
{
    private IRepository _repository;

    public HomeController(IRepository repository)
    {
        _repository = repository;
    }
    
    public IActionResult Index()
    {
        var orders = _repository.GetListOrders();
        return View(orders);
    }
    
    public IActionResult FormOrder(int id)
    {
        var order = _repository.GetOrder(id);
        return View(order);
    }
}
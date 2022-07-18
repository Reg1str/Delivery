using Delivery.DataAccess.Models;
using Delivery.Services.Repository;
using Delivery.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

[Authorize(Roles = "Admin")]
public class AdminPanelController : Controller
{
    private IRepository _repository;

    public AdminPanelController(IRepository repository)
    {
        _repository = repository;
    }
    public IActionResult Index()
    {
        var orders = _repository.GetListOrders();
        return View(orders);
    }
    
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return View(new OrderViewModel());
        }
        else
        {
            var order = _repository.GetOrder((int)id);
            return View(new OrderViewModel
            {
                Id = order.Id,
                SendersCity = order.SendersCity,
                SendersAddress = order.SendersAddress,
                RecipientsCity = order.RecipientsCity,
                RecipientsAddress = order.RecipientsAddress,
                Weight = order.Weight,
                ReceiptDate = order.ReceiptDate
            });
        }    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(OrderViewModel orderViewModel)
    {
        var order = new FormOrder()
        {
            Id = orderViewModel.Id,
            SendersCity = orderViewModel.SendersCity,
            SendersAddress = orderViewModel.SendersAddress,
            RecipientsCity = orderViewModel.RecipientsCity,
            RecipientsAddress = orderViewModel.RecipientsAddress,
            Weight = orderViewModel.Weight,
            ReceiptDate = orderViewModel.ReceiptDate
        };
        
        if (order.Id > 0)
            _repository.UpdateOrder(order);
        else
            _repository.AddOrder(order);
        
        if (await _repository.SaveChangesAsync())
            return RedirectToAction("Index");
        else
            return View(orderViewModel);
    }
    
    [HttpGet]
    public async Task<IActionResult> Remove(int id)
    {
        _repository.RemoveOrder(id);
        await _repository.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
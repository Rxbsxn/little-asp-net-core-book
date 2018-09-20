using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
  public class TodoController : Controller
  {

    private readonly ITodoItemService _todoItemService;

    public TodoController(ITodoItemService todoItemService)
    {
      _todoItemService = todoItemService;
    }

    public async Task<IActionResult> Index()
    {
      var items = await _todoItemService.GetIncompleteItemsAsync();

      var model = new TodoViewModel()
      {
        Items = items
      };

      return View(model);
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItem(TodoItem newTodo)
    {
      if (!ModelState.IsValid)
        return RedirectToAction("Index");

      var validRequest = await _todoItemService.AddItemAsync(newTodo);

      if (!validRequest)
        return BadRequest("Could not add item.");

      return RedirectToAction("Index");
    }
  }
}

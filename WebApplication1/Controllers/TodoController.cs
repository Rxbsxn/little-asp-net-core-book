using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
  [Authorize]
  public class TodoController : Controller
  {

    private readonly ITodoItemService _todoItemService;
    private readonly UserManager<ApplicationUser> _userManager;

    public TodoController(ITodoItemService todoItemService, UserManager<ApplicationUser> userManager)
    {
      _userManager = userManager;
      _todoItemService = todoItemService;
    }

    public async Task<IActionResult> Index()
    {
      var currentUser = await _userManager.GetUserAsync(User);
      if (currentUser == null) return Challenge();

      var items = await _todoItemService.GetIncompleteItemsAsync(currentUser);

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

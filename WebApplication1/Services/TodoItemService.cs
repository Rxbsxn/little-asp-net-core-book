using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
  public class TodoItemService : ITodoItemService
  {
    private readonly ApplicationDbContext _context;

    public TodoItemService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<bool> AddItemAsync(TodoItem newTodo)
    {
      newTodo.Id = Guid.NewGuid();
      newTodo.IsDone = false;
      //newTodo.DueAt = DateTimeOffset.Now.AddDays(3);

      _context.Items.Add(newTodo);

      var saveResult = await _context.SaveChangesAsync();

      return saveResult == 1;
    }

    public async Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser user)
    {
      var items = await _context.Items
        .Where(x => x.IsDone == false)
         .ToArrayAsync();
      return items;
    }
  }
}

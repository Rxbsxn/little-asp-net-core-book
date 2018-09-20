using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
  public interface ITodoItemService
  {
    Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser user);
    Task<bool> AddItemAsync(TodoItem newTodo);
  }
}
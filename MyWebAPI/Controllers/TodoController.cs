using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Models;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Authorization;

namespace MyWebAPI.Controllers
{
    [Route("api/Todos")]
    public class TodoController : Controller
    {

        public TodoController()
        {
            storage.Add(new TodoItem
            {
                Id = 1,
                IsComplete = true,
                Name = "Do something 1"
            });
            storage.Add(new TodoItem
            {
                Id = 2,
                IsComplete = false,
                Name = "Do something 2"
            });
            storage.Add(new TodoItem
            {
                Id = 3,
                IsComplete = true,
                Name = "Do something 3"
            });
        }
        //TodoTempStorage storage = new TodoTempStorage();
        List<TodoItem> storage = new List<TodoItem>();

        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> GetTodoItems()
        {
            if (storage == null)
                return NotFound();
            return storage;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = storage.Find(x => x.Id == id);
            if (todoItem == null)
                return NotFound();
            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest();
            }
            if (storage.Find(x => x == todoItem) == null)
            {
                return BadRequest();
            }
            todoItem.Id = storage.Capacity + 1;
            todoItem.IsComplete = false;
            storage.Add(todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutTodoItem(long id, TodoItem item)
        {
            if (id != item.Id)
                return BadRequest();
            int index = storage.FindIndex(x => x == item);
            if (index != -1)
            {
                storage[index].Id = item.Id;
                storage[index].Name = item.Name;
                storage[index].IsComplete = item.IsComplete;
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var todoItemIndex = storage.FindIndex(x => x.Id == id);
            if (todoItemIndex == -1)
            {
                return NotFound();
            }
            TodoItem DeletedItem = storage[todoItemIndex];
            storage.Remove(storage[todoItemIndex]);
            return DeletedItem;
        }
    }
}
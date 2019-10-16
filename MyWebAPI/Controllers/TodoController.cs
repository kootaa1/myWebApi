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
        TodoTempStorage storage = TodoTempStorage.GetInstance();
        public TodoController()
        {
        }
        //TodoTempStorage storage = new TodoTempStorage();

        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> GetTodoItems()
        {
            var result = storage.GetItems();
            if (result == null)
                return NotFound();
            else return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = storage.GetItem(id);
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
            if(!storage.AddItem(todoItem))
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutTodoItem(long id, TodoItem item)
        {
            if (id != item.Id)
                return BadRequest();
            if (storage.UpdateItem(item))
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var deletedItem = storage.DeleteItem(id);
            if (deletedItem == null)
            {
                return NotFound();
            }
            return deletedItem;
        }
    }
}
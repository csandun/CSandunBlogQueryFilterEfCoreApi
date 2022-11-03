using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSandunBlogQueryFilterEfCoreApi.Dtos;
using CSandunBlogQueryFilterEfCoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSandunBlogQueryFilterEfCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            // return await _context.TodoItems.Where(o => !o.IsDelete)
            //     .Select(x => ItemToDTO(x))
            //     .ToListAsync();

            return await _context.TodoItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            //var todoItem = await _context.TodoItems.Where(o => !o.IsDelete). FirstOrDefaultAsync(o => o.Id == id);

            // removing isdelete filter
            var todoItem = await _context.TodoItems.FirstOrDefaultAsync(o => o.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }


            //var todoItem = await _context.TodoItems.Where(o 
            //     => !o.IsDelete). FirstOrDefaultAsync(o => o.Id == id);

            var todoItem = await _context.TodoItems.FirstOrDefaultAsync(o => o.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new {id = todoItem.Id},
                ItemToDTO(todoItem));
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            //var todoItem = await _context.TodoItems.Where(o => !o.IsDelete). FirstOrDefaultAsync(o => o.Id == id);

            var todoItem = await _context.TodoItems.FirstOrDefaultAsync(o => o.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.IsDelete = true;
            _context.TodoItems.Update(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            //return _context.TodoItems.Any(e => e.Id == id && !e.IsDelete);

            return _context.TodoItems.Any(e => e.Id == id);
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetAllTodoItemsWithDeleted()
        {
            return await _context.TodoItems
                .IgnoreQueryFilters() // Ignore query filter
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        /// <summary>
        /// Do not use explicitly without IgnoreQueryFilters()
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> Do_Not_use_Explicite_Filter_With_Query_Filter()
        {
            
            /// This is wrong. DONOT use like explicitly 
            var result = await _context.TodoItems
                .Where(o => o.IsDelete)// Ignore query filter
                .Select(x => ItemToDTO(x))
                .ToListAsync();
            
            
            // If you want use explicitly, First USE IgnoreQueryFilters(). Then use where filter in the query.
            result =  await _context.TodoItems
                .IgnoreQueryFilters()
                .Where(o => o.IsDelete)// Ignore query filter
                .Select(x => ItemToDTO(x))
                .ToListAsync();

            return result;
        }
    }
}
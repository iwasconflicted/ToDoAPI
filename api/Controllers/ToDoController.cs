using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {

      private readonly AppDbContext _context;
      public ToDoController(AppDbContext context)
      {
        _context = context;
      }
      
      [HttpGet]

      public async Task<IEnumerable<ToDoList>> getToDo()
      {
        var doList = await _context.ToDoLists.AsNoTracking().ToListAsync();
        return doList;
      }

      [HttpPost]

      public async Task<IActionResult> Create(ToDoList doList)
      {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _context.AddAsync(doList);

        var result = await _context.SaveChangesAsync();
        if(result > 0)
        {
            return Ok();
        }
        return BadRequest();
      }

      [HttpDelete("{id:int}")]

      public async Task<IActionResult> Delete(int id)
      {
        var doList = await _context.ToDoLists.FindAsync();
        if(doList == null)
        {
          return NotFound();
        }

        _context.Remove(doList);

        var result = await _context.SaveChangesAsync();

        if(result > 0)
        {
          return Ok("Task was deleted");
        }
        return BadRequest("Cannot find Task");
      }

     [HttpGet("{id:int}")]

     public async Task<ActionResult<ToDoList>> GetTask(int id)
     {
        var doList = await _context.ToDoLists.FindAsync(id);
        if(doList == null)
        {
            return NotFound("Sorry, Task was not found");
        }
        return Ok(doList);
     }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> TaskEdit(int id, ToDoList doList)
    {
        var doListFromDb = await _context.ToDoLists.FindAsync(id);
        if(doListFromDb == null)
        {
            return BadRequest("Task not found");
        }
        doListFromDb.Title = doList.Title;
        doListFromDb.Description = doList.Description;
        doListFromDb.IsCompleted = doList.IsCompleted;
  

        var result = await _context.SaveChangesAsync();

        if(result > 0)
        {
            return Ok("Task was edited");
        }
        return BadRequest("Unable to update data");
    }


    }
}
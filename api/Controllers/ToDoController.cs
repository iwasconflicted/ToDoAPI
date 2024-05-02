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












    }
}
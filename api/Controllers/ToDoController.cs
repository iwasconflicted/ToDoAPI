using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;

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
      
    }
}
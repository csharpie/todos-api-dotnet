using Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Todo.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    // GET
    [HttpGet("/")]
    public IEnumerable<Todo> GetAll()
    {
        return new List<Todo>();
    }
}
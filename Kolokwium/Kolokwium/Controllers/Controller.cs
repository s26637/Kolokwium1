using Kolokwium.Models;
using Kolokwium.Rep;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium.Controllers;

public class Controller : ControllerBase
{
    private readonly IBookRep _bookRep;
    public Controller(IBookRep bookRep)
    {
        _bookRep = bookRep;
    }

    [HttpGet("api/books/{id}/genres")]
    public async Task<IActionResult> GetBook(int id)
    {
        
        var book = await _bookRep.GetBook(id);
            
        return Ok(book);
    }
    
    
    [HttpPost("api/books")]
    public async Task<IActionResult> AddBook(string title)
    {

        Book book1 = new Book()
        {
            Id = 2,
            Title = title

        };
        
        var book = await _bookRep.AddBook(book1);
        
        return Created(Request.Path.Value ?? "api/books", book1);
    }
    
}
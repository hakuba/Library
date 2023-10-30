
using Microsoft.AspNetCore.Mvc;
using Library.Services;
using Library.Libs;

[Route("api/books")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly BookService _bookService;

    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var books = await _bookService.GetAll();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var book = await _bookService.GetById(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RequestLibs.AddNewBook req)
    {
        
        var result = await _bookService.Add(req);
        if (result)
            return Ok(new { message = "Success"});
        else
        {
            return BadRequest(new { message = "Failed to add new book. Please check the data." });
        } 
            
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] string id, [FromBody] RequestLibs.UpdateBook req)
    {
        if (id != req.Id.ToString())
        {
            return BadRequest();
        }

        var success = await _bookService.Update(req);
        if (!success)
        {
            return NotFound();
        }

        return Ok(new { message = "Successfully updating data with id: " + id });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _bookService.Delete(id);
        if (!success)
        {
            return NotFound();
        }
        return Ok(new { message = "Successfully deleting data with id: " + id });
    }
    
}

using Microsoft.AspNetCore.Mvc;
using Library.Libs;
using Library.Services;

[Route("api/library")]
[ApiController]
public class LibraryController : ControllerBase
{
    private readonly LibraryService _libraryService;

    public LibraryController(LibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var librarys = await _libraryService.GetAll();
        return Ok(librarys);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var library = await _libraryService.GetById(id);
        if (library == null)
        {
            return NotFound();
        }
        return Ok(library);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RequestLibs.AddNewLibrary req)
    {

        var result = await _libraryService.Add(req);
        if (result)
            return Ok(new { message = "Success" });
        else
        {
            return BadRequest(new { message = "Failed to add new library. Please check the data." });
        }

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] string id, [FromBody] RequestLibs.UpdateLibrary req)
    {

        var success = await _libraryService.Update(id, req);
        if (!success)
        {
            return BadRequest();
        }

        return Ok(new { message = "Successfully updating data with id: " + id });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var success = await _libraryService.Delete(id);
        if (!success)
        {
            return NotFound();
        }

        return Ok(new { message = "Successfully deleting data with id: " + id });
    }

    [HttpGet("{id}/book")]
    public async Task<IActionResult> ListBook([FromRoute] Guid id)
    {

        var books = await _libraryService.ListBook(id);
        return Ok(books);
    }
    [HttpGet("{id}/book/{bookId}")]
    public async Task<IActionResult> GetRelationById([FromRoute] Guid id, [FromRoute] Guid bookId)
    {

        var rel = await _libraryService.GetRelationById(id, bookId);
        return Ok(rel);
        

    }
    [HttpPost("{id}/book/")]
    public async Task<IActionResult> AddBook([FromRoute] Guid id, [FromBody] RequestLibs.AddNewBookLibrary req)
    {

        var result = await _libraryService.AddBook(id,req);
        if (result)
            return Ok(new { message = "Success" });
        else
        {
            return BadRequest(new { message = "Failed to add new book to library. Please check the data." });
        }

    }
    [HttpPut("{id}/book/{bookId}")]
    public async Task<IActionResult> UpdateRelation([FromRoute] Guid id, [FromRoute] Guid bookId, [FromBody] RequestLibs.UpdateBookLibrary req)
    {

        var rel = await _libraryService.UpdateBook(id, bookId,req);
        return Ok(rel);


    }
    [HttpDelete("{id}/book/{bookId}")]
    public async Task<IActionResult> DeleteRelation([FromRoute] Guid id, [FromRoute] Guid bookId)
    {

        var rel = await _libraryService.DeleteBook(id, bookId);
        return Ok(rel);


    }

}

using Microsoft.AspNetCore.Mvc;
using Library.Libs;
using Library.Services;

[Route("api/author")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly AuthorService _authorService;

    public AuthorController(AuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var authors = await _authorService.GetAll();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var author = await _authorService.GetById(id);
        if (author == null)
        {
            return NotFound();
        }
        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RequestLibs.AddNewAuthor req)
    {

        var result = await _authorService.Add(req);
        if (result)
            return Ok(new { message = "Success" });
        else
        {
            return BadRequest(new { message = "Failed to add new author. Please check the data." });
        }

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] string id, [FromBody] RequestLibs.UpdateAuthor req)
    {
        if (id != req.Id.ToString())
        {
            return BadRequest();
        }

        var success = await _authorService.Update(req);
        if (!success)
        {
            return NotFound();
        }

        return Ok(new { message = "Successfully updating data with id: "+id});
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var success = await _authorService.Delete(id);
        if (!success)
        {
            return NotFound();
        }

        return Ok(new { message = "Successfully deleting data with id: "+id});
    }

}
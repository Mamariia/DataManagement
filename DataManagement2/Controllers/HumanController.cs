using DataManagement1;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class MyEntityController : ControllerBase
{
    private readonly IRepository<Person> _repository;

    public MyEntityController(IRepository<Person> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var persons = _repository.GetAll();
        return Ok(persons);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var entity = _repository.GetById(id);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(entity);
    }

    [HttpPost]
    public IActionResult AddEntity([FromBody] Person person)
    {
        if (person == null)
        {
            return BadRequest("Entity cannot be null");
        }

        _repository.Add(person);

        return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEntity(int id)
    {
        var person = _repository.GetById(id);

        if (person == null)
        {
            return NotFound();
        }

        _repository.Delete(id);

        return NoContent();
    }
}
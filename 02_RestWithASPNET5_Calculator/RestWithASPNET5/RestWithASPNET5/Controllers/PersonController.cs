using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNET5.Model;
using RestWithASPNET5.Services.Implementations;
using System.Collections.Generic;

namespace RestWithASPNET5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
               
        List<Person> people = new List<Person>();
        private readonly ILogger<PersonController> _logger;
        private IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
            Person p1 = new Person(1, "Alexandre", "Lopes", "Curitiba", "Masculino");
            Person p2 = new Person(2, "João", "Tobias", "Colombo", "Masculino");
            Person p3 = new Person(3, "Maria", "Regina", "Curitiba", "Feminino");
            people.Add(p1);
            people.Add(p2);
            people.Add(p3);
    }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll(people));
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personService.FindById(people, id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            people.Add(person);
            return Ok(_personService.Create(people, person));
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            return Ok(_personService.Update(people, person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(people, id);
            return NoContent();
        }
    }
}

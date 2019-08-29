using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiApplication;
using WebApiApplication.Models;

namespace WebApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly AppDatabaseContext _context;

        public PersonController(AppDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Person
        [HttpGet]
        public IEnumerable<Person> GetPersons() //получаем всех Пользователей
        {
            return _context.Persons.Include(p => p.skills).ToList();
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson([FromRoute] long id) //Получаем пользователя по ID
        {
            if (!PersonExists(id))
            {
                return BadRequest(); //400
            }

            Person person = _context.Persons.Include(p => p.skills).Where(c => c.id == id).FirstOrDefault();

            if (person == null)
            {
                return NotFound(); // 404
            }

            return Ok(person);//200
        }

        // POST: api/Person/5
        [HttpPost("{id}")]
        //По идее post - создает ресурс, а put - обновляет или редактирует, 
        //но раз в ТЗ написано что put - должен создавать, 
        //а post  - редактировать, так тому и быть ) 
        public async Task<IActionResult> Put(long id, [FromBody] Person requestPerson)
        {
            if (!PersonExists(id)) {
                return NotFound();
            }
            var person = _context.Persons.Include(p => p.skills).Where(p => p.id == id).FirstOrDefault();
            var skillRemove = _context.Skills.Where(s => s.PersonId == person.id).ToList();

            foreach (var i in skillRemove) {
                _context.Skills.Remove(i);
                _context.SaveChanges();
            }

            foreach (var i in requestPerson.skills){
                Skill skill = new Skill();
                skill.level = i.level;
                skill.name = i.name;
                skill.PersonId = person.id;
                _context.Skills.Add(skill);
                person.skills.Add(skill);
                _context.SaveChanges();
            }

            person.name = requestPerson.name;
            person.displayName = requestPerson.displayName;
            try {
                  _context.Update(person);
                  await _context.SaveChangesAsync();

                return Ok(person);
            } catch {
                return BadRequest();
            } 
        }

        // PUT: api/Person
        [HttpPut] 
        public async Task<IActionResult> PostPerson([FromBody] Person person) //Создаем пользователя
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.id }, person);
        }


        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {

            Person person = _context.Persons.Where(c => c.id == id).FirstOrDefault();

            _context.Entry(person).Collection(c => c.skills).Load();

            _context.Persons.Remove(person);
            _context.SaveChanges();
            return Ok(person);
        }

        private bool PersonExists(long id)
        {
            return _context.Persons.Any(e => e.id == id); // проверяем есть ли в БД такой id
        }
    }
}
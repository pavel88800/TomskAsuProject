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
    public class SkillController : ControllerBase
    {
        private readonly AppDatabaseContext _context;

        public SkillController(AppDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Skill
        [HttpGet]
        public IEnumerable<Skill> GetSkills()
        {
            return _context.Skills;
        }

        // GET: api/Skill/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkill([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        // PUT: api/Skill/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkill([FromRoute] int id, [FromBody] Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != skill.id)
            {
                return BadRequest();
            }

            _context.Entry(skill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Skill
        [HttpPost]
        public async Task<IActionResult> PostSkill([FromBody] Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkill", new { id = skill.id }, skill);
        }

        // DELETE: api/Skill/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return Ok(skill);
        }

        private bool SkillExists(int id)
        {
            return _context.Skills.Any(e => e.id == id);
        }
    }
}
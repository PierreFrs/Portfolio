using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Services;

namespace Portfolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly SkillService _skillService;

        public SkillController(SkillService skillService)
        {
            _skillService = skillService;
        }

        // GET: api/Skill
        [HttpGet]
        public async Task<IActionResult> GetSkills()
        {
            var skills = await _skillService.GetAllSkillsAsync();
            return Ok(skills);
        }

        // GET: api/Skill/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkill(string id)
        {
            var skill = await _skillService.GetSkillByIdAsync(id);
            if (skill == null)
            {
                return NotFound();
            }
            return Ok(skill);
        }

        // POST: api/Skill
        [HttpPost]
        public async Task<IActionResult> CreateSkill([FromBody] Skill skill)
        {
            await _skillService.AddSkillAsync(skill);
            return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, skill);
        }

        // DELETE: api/Skill/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(string id)
        {
            var deletedSkill = await _skillService.DeleteSkillAsync(id);
            if (deletedSkill == null)
            {
                return NotFound();
            }
            return Ok(deletedSkill);
        }
    }
}

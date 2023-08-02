using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Services;
using System.Threading.Tasks;

namespace Portfolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AboutMeController : ControllerBase
    {
        private readonly AboutMeService _aboutMeService;

        public AboutMeController(AboutMeService aboutMeService)
        {
            _aboutMeService = aboutMeService;
        }

        // GET: api/AboutMe/english
        [HttpGet("english")]
        public async Task<IActionResult> GetAboutMeEnglish()
        {
            var aboutMe = await _aboutMeService.GetAboutMeAsync("english");
            if (aboutMe == null)
            {
                return NotFound();
            }
            return Ok(aboutMe);
        }

        // GET: api/AboutMe/french
        [HttpGet("french")]
        public async Task<IActionResult> GetAboutMeFrench()
        {
            var aboutMe = await _aboutMeService.GetAboutMeAsync("french");
            if (aboutMe == null)
            {
                return NotFound();
            }
            return Ok(aboutMe);
        }

        // POST: api/AboutMe
        [HttpPost]
        public async Task<IActionResult> CreateAboutMe([FromBody] AboutMe aboutMe)
        {
            await _aboutMeService.AddAboutMeAsync(aboutMe);
            return CreatedAtAction(nameof(GetAboutMeEnglish), new { }, aboutMe);
        }

        // PUT: api/AboutMe
        [HttpPut]
        public async Task<IActionResult> UpdateAboutMe([FromBody] AboutMe aboutMe)
        {
            await _aboutMeService.UpdateAboutMeAsync(aboutMe);
            return Ok();
        }

        // DELETE: api/AboutMe/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAboutMe(string id)
        {
            await _aboutMeService.DeleteAboutMeAsync(id);
            return Ok();
        }
    }
}

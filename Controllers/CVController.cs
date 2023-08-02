using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Services;

namespace Portfolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CVController : ControllerBase
    {
        private readonly CVService _cvService;
        public CVController(CVService cvService)
        {
            _cvService = cvService;
        }

        // GET: api/CV/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCV(string id)
        {
            var cv = await _cvService.GetCVByIdAsync(id);
            if (cv == null)
            {
                return NotFound();
            }
            return Ok(cv);
        }

        // POST: api/CV
        [HttpPost]
        public async Task<IActionResult> CreateCV([FromBody] CV cv)
        {
            await _cvService.AddCVAsync(cv);
            return CreatedAtAction(nameof(GetCV), new { id = cv.Id }, cv);
        }


        // DELETE: api/CV/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCV(string id)
        {
            var deletedCV = await _cvService.DeleteCVAsync(id);
            if (deletedCV == null)
            {
                return NotFound();
            }
            return Ok(deletedCV);
        }
    }
}
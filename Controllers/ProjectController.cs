using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Services;
using System.Threading.Tasks;

namespace Portfolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        // GET: api/Project/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(string id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        // POST: api/Project
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] Project project)
        {
            await _projectService.AddProjectAsync(project);
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        // PUT: api/Project/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProject(string id, [FromBody] Project project)
        {
            // The 'Url' property will be bound from the request body
            var updatedProject = _projectService.UpdateProjectAsync(id, project);
            if (updatedProject == null)
            {
                return NotFound();
            }
            return Ok(updatedProject);
        }

        // DELETE: api/Project/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(string id)
        {
            var deletedProject = await _projectService.DeleteProjectAsync(id);
            if (deletedProject == null)
            {
                return NotFound();
            }
            return Ok(deletedProject);
        }
    }
}

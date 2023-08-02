using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Services;

namespace Portfolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPictureController : ControllerBase
    {
        private readonly UserPictureService _userPictureService;

        public UserPictureController(UserPictureService userPictureService)
        {
            _userPictureService = userPictureService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPicture()
        {
            var userPicture = await _userPictureService.GetUserPictureAsync();
            return Ok(userPicture);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserPicture([FromBody] string url)
        {
            await _userPictureService.UpdateUserPictureAsync(url);
            return Ok();
        }
    }
}

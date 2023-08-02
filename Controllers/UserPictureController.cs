using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("upload")]
        public async Task<IActionResult> UploadUserPicture(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please select a file to upload.");
            }

            try
            {
                // Get the Amazon S3 client from the service collection
                var s3Client = HttpContext.RequestServices.GetService<IAmazonS3>();

                // Generate a unique file name for the S3 bucket
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                // Define the bucket name and file key (file path) for the uploaded picture
                var bucketName = "myportfoliopictures";
                var fileKey = "user-picture/" + uniqueFileName;

                // Upload the picture to S3
                using (var stream = file.OpenReadStream())
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = fileKey,
                        InputStream = stream,
                        CannedACL = S3CannedACL.PublicRead // Set ACL to public read to make the picture publicly accessible
                    };

                    var response = await s3Client.PutObjectAsync(request);
                }

                // If the upload is successful, save the picture URL to MongoDB
                var pictureUrl = $"https://{bucketName}.s3.amazonaws.com/{fileKey}";
        await _userPictureService.UpdateUserPictureAsync(pictureUrl);

                return Ok("Picture uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while uploading the picture: {ex.Message}");
            }
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

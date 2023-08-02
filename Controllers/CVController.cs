using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
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
        private readonly IAmazonS3 _s3Client; // Add the S3 client dependency

        public CVController(CVService cvService, IAmazonS3 s3Client)
        {
            _cvService = cvService;
            _s3Client = s3Client;
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

        // POST: api/CV/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadCV(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please select a file to upload.");
            }

            try
            {
                // Generate a unique file name for the S3 bucket
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                // Define the S3 bucket name and file key (file path) for the uploaded file
                var bucketName = "myportfoliopictures";
                var fileKey = "cv/" + uniqueFileName; // Change "cv/" to your desired folder structure

                // Upload the file to S3
                using (var stream = file.OpenReadStream())
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = fileKey,
                        InputStream = stream,
                        CannedACL = S3CannedACL.PublicRead // Set ACL to public read to make the file publicly accessible
                    };

                    var response = await _s3Client.PutObjectAsync(request);
                }

                // Create a new CV object with the file URL and other details
                var cv = new CV
                {
                    FilePath = fileKey
                };

                // Save the CV object to MongoDB
                await _cvService.AddCVAsync(cv);

                return Ok("File uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while uploading the file: {ex.Message}");
            }
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

using CRM.BLL.Common;
using CRM.BLL.DTOs.Requests;
using CRM.BLL.DTOs.Responese;
using CRM.BLL.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly ITestimonialService _testimonialService;
        private readonly IFileService _fileService;

        public TestimonialsController(
            ITestimonialService testimonialService,
            IFileService fileService)
        {
            _testimonialService = testimonialService;
            _fileService = fileService;
        }

        /// <summary>
        /// Get all published testimonials (Public)
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<TestimonialDto>>>> GetPublished()
        {
            try
            {
                var testimonials = await _testimonialService.GetPublishedAsync();
                return Ok(ApiResponse<IEnumerable<TestimonialDto>>.Success(
                    testimonials,
                    "Testimonials retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<TestimonialDto>>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Create new testimonial
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<TestimonialResponseDto>>> Create(
            [FromBody] CreateTestimonialDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponse<TestimonialResponseDto>.Failure(errors));
                }

                var result = await _testimonialService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetPublished),
                    new { id = result.Id },
                    ApiResponse<TestimonialResponseDto>.Success(
                        result,
                        "Testimonial created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<TestimonialResponseDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Update testimonial
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> Update(
            int id,
            [FromBody] UpdateTestimonialDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponse<bool>.Failure(errors));
                }

                var result = await _testimonialService.UpdateAsync(id, dto);

                if (!result)
                {
                    return NotFound(ApiResponse<bool>.Failure(
                        $"Testimonial with ID {id} not found"));
                }

                return Ok(ApiResponse<bool>.Success(
                    true,
                    "Testimonial updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Delete testimonial
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            try
            {
                var result = await _testimonialService.DeleteAsync(id);

                if (!result)
                {
                    return NotFound(ApiResponse<bool>.Failure(
                        $"Testimonial with ID {id} not found"));
                }

                return Ok(ApiResponse<bool>.Success(
                    true,
                    "Testimonial deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Publish testimonial
        /// </summary>
        [HttpPut("{id}/publish")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> Publish(int id)
        {
            try
            {
                var result = await _testimonialService.PublishAsync(id);

                if (!result)
                {
                    return NotFound(ApiResponse<bool>.Failure(
                        $"Testimonial with ID {id} not found"));
                }

                return Ok(ApiResponse<bool>.Success(
                    true,
                    "Testimonial published successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Unpublish testimonial
        /// </summary>
        [HttpPut("{id}/unpublish")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> Unpublish(int id)
        {
            try
            {
                var result = await _testimonialService.UnpublishAsync(id);

                if (!result)
                {
                    return NotFound(ApiResponse<bool>.Failure(
                        $"Testimonial with ID {id} not found"));
                }

                return Ok(ApiResponse<bool>.Success(
                    true,
                    "Testimonial unpublished successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Upload client image for testimonial
        /// </summary>
        [HttpPost("{id}/image")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<FileUploadResponseDto>>> UploadClientImage(
            int id,
            IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest(ApiResponse<FileUploadResponseDto>.Failure(
                        "Please select an image file"));
                }

                if (!_fileService.ValidateImage(file))
                {
                    return BadRequest(ApiResponse<FileUploadResponseDto>.Failure(
                        "Invalid image format or file size exceeds limit"));
                }

                var imagePath = await _fileService.UploadImageAsync(file, "testimonials");

                var response = new FileUploadResponseDto
                {
                    FilePath = imagePath,
                    FileName = file.FileName,
                    FileSize = file.Length,
                    ContentType = file.ContentType,
                    UploadedAt = DateTime.UtcNow
                };

                return Ok(ApiResponse<FileUploadResponseDto>.Success(
                    response,
                    "Client image uploaded successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<FileUploadResponseDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }
    }
}
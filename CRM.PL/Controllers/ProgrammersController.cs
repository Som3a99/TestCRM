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
    public class ProgrammersController : ControllerBase
    {
        private readonly IProgrammerService _programmerService;
        private readonly IFileService _fileService;

        public ProgrammersController(
            IProgrammerService programmerService,
            IFileService fileService)
        {
            _programmerService = programmerService;
            _fileService = fileService;
        }

        /// <summary>
        /// Get all active programmers (Public)
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProgrammerCardDto>>>> GetActive()
        {
            try
            {
                var programmers = await _programmerService.GetActiveAsync();
                return Ok(ApiResponse<IEnumerable<ProgrammerCardDto>>.Success(
                    programmers,
                    "Programmers retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<ProgrammerCardDto>>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Get programmer by ID with projects (Public)
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<ProgrammerDetailsDto>>> GetById(int id)
        {
            try
            {
                var programmer = await _programmerService.GetByIdAsync(id);

                if (programmer == null)
                {
                    return NotFound(ApiResponse<ProgrammerDetailsDto>.Failure(
                        $"Programmer with ID {id} not found"));
                }

                return Ok(ApiResponse<ProgrammerDetailsDto>.Success(
                    programmer,
                    "Programmer retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ProgrammerDetailsDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Create new programmer
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<ProgrammerResponseDto>>> Create(
            [FromBody] CreateProgrammerDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponse<ProgrammerResponseDto>.Failure(errors));
                }

                var result = await _programmerService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = result.Id },
                    ApiResponse<ProgrammerResponseDto>.Success(
                        result,
                        "Programmer created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ProgrammerResponseDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Update programmer
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> Update(
            int id,
            [FromBody] UpdateProgrammerDto dto)
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

                var result = await _programmerService.UpdateAsync(id, dto);

                if (!result)
                {
                    return NotFound(ApiResponse<bool>.Failure(
                        $"Programmer with ID {id} not found"));
                }

                return Ok(ApiResponse<bool>.Success(
                    true,
                    "Programmer updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Deactivate programmer
        /// </summary>
        [HttpPut("{id}/deactivate")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> Deactivate(int id)
        {
            try
            {
                var result = await _programmerService.DeactivateAsync(id);

                if (!result)
                {
                    return NotFound(ApiResponse<bool>.Failure(
                        $"Programmer with ID {id} not found"));
                }

                return Ok(ApiResponse<bool>.Success(
                    true,
                    "Programmer deactivated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Upload programmer image
        /// </summary>
        [HttpPost("{id}/image")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<ProgrammerImageUploadDto>>> UploadImage(
            int id,
            IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest(ApiResponse<ProgrammerImageUploadDto>.Failure(
                        "Please select an image file"));
                }

                if (!_fileService.ValidateImage(file))
                {
                    return BadRequest(ApiResponse<ProgrammerImageUploadDto>.Failure(
                        "Invalid image format or file size exceeds limit"));
                }

                // Get programmer to check if exists and get old image path
                var programmer = await _programmerService.GetByIdAsync(id);
                if (programmer == null)
                {
                    return NotFound(ApiResponse<ProgrammerImageUploadDto>.Failure(
                        $"Programmer with ID {id} not found"));
                }

                // Store old image path for cleanup
                var oldImagePath = programmer.ImagePath;

                // Upload new image
                var imagePath = await _fileService.UploadImageAsync(file, "programmers");

                // Update programmer record with new image path
                var updateResult = await _programmerService.UpdateImageAsync(id, imagePath);

                if (!updateResult)
                {
                    // If update fails, delete the uploaded file to prevent orphans
                    await _fileService.DeleteFileAsync(imagePath);
                    return StatusCode(500, ApiResponse<ProgrammerImageUploadDto>.Failure(
                        "Failed to update programmer image record"));
                }

                // Delete old image if it exists and is not the default
                if (!string.IsNullOrEmpty(oldImagePath) &&
                    oldImagePath != "/uploads/programmers/default.jpg")
                {
                    await _fileService.DeleteFileAsync(oldImagePath);
                }

                var response = new ProgrammerImageUploadDto
                {
                    ProgrammerId = id,
                    FilePath = imagePath,
                    FileName = file.FileName,
                    FileSize = file.Length,
                    ContentType = file.ContentType,
                    UploadedAt = DateTime.UtcNow
                };

                return Ok(ApiResponse<ProgrammerImageUploadDto>.Success(
                    response,
                    "Image uploaded successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ProgrammerImageUploadDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }
    }
}
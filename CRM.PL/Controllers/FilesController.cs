using CRM.BLL.Common;
using CRM.BLL.DTOs.Responese;
using CRM.BLL.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Upload an image file
        /// </summary>
        [HttpPost("image")]
        public async Task<ActionResult<ApiResponse<FileUploadResponseDto>>> UploadImage(
            IFormFile file,
            [FromQuery] string folder = "general")
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
                        "Invalid image format or file size exceeds 5MB limit. Supported formats: JPG, JPEG, PNG, GIF, WebP"));
                }

                var filePath = await _fileService.UploadImageAsync(file, folder);

                var response = new FileUploadResponseDto
                {
                    FilePath = filePath,
                    FileName = file.FileName,
                    FileSize = file.Length,
                    ContentType = file.ContentType,
                    UploadedAt = DateTime.UtcNow
                };

                return Ok(ApiResponse<FileUploadResponseDto>.Success(
                    response,
                    "Image uploaded successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<FileUploadResponseDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Upload a video file
        /// </summary>
        [HttpPost("video")]
        public async Task<ActionResult<ApiResponse<FileUploadResponseDto>>> UploadVideo(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest(ApiResponse<FileUploadResponseDto>.Failure(
                        "Please select a video file"));
                }

                if (!_fileService.ValidateVideo(file))
                {
                    return BadRequest(ApiResponse<FileUploadResponseDto>.Failure(
                        "Invalid video format or file size exceeds 50MB limit. Supported formats: MP4, WebM, MOV"));
                }

                var filePath = await _fileService.UploadVideoAsync(file);

                var response = new FileUploadResponseDto
                {
                    FilePath = filePath,
                    FileName = file.FileName,
                    FileSize = file.Length,
                    ContentType = file.ContentType,
                    UploadedAt = DateTime.UtcNow
                };

                return Ok(ApiResponse<FileUploadResponseDto>.Success(
                    response,
                    "Video uploaded successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<FileUploadResponseDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Upload an audio file
        /// </summary>
        [HttpPost("audio")]
        public async Task<ActionResult<ApiResponse<FileUploadResponseDto>>> UploadAudio(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest(ApiResponse<FileUploadResponseDto>.Failure(
                        "Please select an audio file"));
                }

                if (!_fileService.ValidateAudio(file))
                {
                    return BadRequest(ApiResponse<FileUploadResponseDto>.Failure(
                        "Invalid audio format or file size exceeds 10MB limit. Supported formats: MP3, WAV, OGG"));
                }

                var filePath = await _fileService.UploadAudioAsync(file);

                var response = new FileUploadResponseDto
                {
                    FilePath = filePath,
                    FileName = file.FileName,
                    FileSize = file.Length,
                    ContentType = file.ContentType,
                    UploadedAt = DateTime.UtcNow
                };

                return Ok(ApiResponse<FileUploadResponseDto>.Success(
                    response,
                    "Audio uploaded successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<FileUploadResponseDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Upload multiple images at once
        /// </summary>
        [HttpPost("images/batch")]
        public async Task<ActionResult<ApiResponse<BatchFileUploadResponseDto>>> UploadMultipleImages(
            List<IFormFile> files,
            [FromQuery] string folder = "general")
        {
            try
            {
                if (files == null || !files.Any())
                {
                    return BadRequest(ApiResponse<BatchFileUploadResponseDto>.Failure(
                        "Please select at least one image file"));
                }

                var uploadedFiles = new List<FileUploadResponseDto>();
                var failedFiles = new List<FailedFileUploadDto>();

                foreach (var file in files)
                {
                    try
                    {
                        if (file.Length == 0)
                        {
                            failedFiles.Add(new FailedFileUploadDto
                            {
                                FileName = file.FileName,
                                Reason = "File is empty"
                            });
                            continue;
                        }

                        if (!_fileService.ValidateImage(file))
                        {
                            failedFiles.Add(new FailedFileUploadDto
                            {
                                FileName = file.FileName,
                                Reason = "Invalid format or size exceeds limit"
                            });
                            continue;
                        }

                        var filePath = await _fileService.UploadImageAsync(file, folder);

                        uploadedFiles.Add(new FileUploadResponseDto
                        {
                            FilePath = filePath,
                            FileName = file.FileName,
                            FileSize = file.Length,
                            ContentType = file.ContentType,
                            UploadedAt = DateTime.UtcNow
                        });
                    }
                    catch (Exception ex)
                    {
                        failedFiles.Add(new FailedFileUploadDto
                        {
                            FileName = file.FileName,
                            Reason = ex.Message
                        });
                    }
                }

                var response = new BatchFileUploadResponseDto
                {
                    SuccessfulUploads = uploadedFiles,
                    FailedUploads = failedFiles,
                    TotalFiles = files.Count,
                    SuccessCount = uploadedFiles.Count,
                    FailureCount = failedFiles.Count
                };

                return Ok(ApiResponse<BatchFileUploadResponseDto>.Success(
                    response,
                    $"{uploadedFiles.Count} of {files.Count} files uploaded successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<BatchFileUploadResponseDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Delete a file
        /// </summary>
        [HttpDelete]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteFile([FromQuery] string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    return BadRequest(ApiResponse<bool>.Failure(
                        "File path is required"));
                }

                var result = await _fileService.DeleteFileAsync(filePath);

                if (!result)
                {
                    return NotFound(ApiResponse<bool>.Failure(
                        "File not found or already deleted"));
                }

                return Ok(ApiResponse<bool>.Success(
                    true,
                    "File deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Validate file without uploading
        /// </summary>
        [HttpPost("validate")]
        public ActionResult<ApiResponse<FileValidationResponseDto>> ValidateFile(
            IFormFile file,
            [FromQuery] string fileType = "image")
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest(ApiResponse<FileValidationResponseDto>.Failure(
                        "Please select a file"));
                }

                bool isValid = fileType.ToLower() switch
                {
                    "image" => _fileService.ValidateImage(file),
                    "video" => _fileService.ValidateVideo(file),
                    "audio" => _fileService.ValidateAudio(file),
                    _ => false
                };

                var response = new FileValidationResponseDto
                {
                    IsValid = isValid,
                    FileName = file.FileName,
                    FileSize = file.Length,
                    ContentType = file.ContentType,
                    FileType = fileType,
                    ValidationMessage = isValid
                        ? "File is valid and can be uploaded"
                        : "File format or size does not meet requirements"
                };

                return Ok(ApiResponse<FileValidationResponseDto>.Success(
                    response,
                    isValid ? "File validation passed" : "File validation failed"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<FileValidationResponseDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }
    }
}
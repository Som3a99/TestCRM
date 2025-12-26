using CRM.BLL.Common;
using CRM.BLL.DTOs.Requests;
using CRM.BLL.DTOs.Responese;
using CRM.BLL.Interfaces.Services;
using CRM.DAL.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InquiriesController : ControllerBase
    {
        private readonly IInquiryService _inquiryService;

        public InquiriesController(IInquiryService inquiryService)
        {
            _inquiryService = inquiryService;
        }

        /// <summary>
        /// Create new inquiry (Public endpoint)
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<InquiryResponseDto>>> Create(
            [FromBody] CreateInquiryDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponse<InquiryResponseDto>.Failure(errors));
                }

                var result = await _inquiryService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = result.Id },
                    ApiResponse<InquiryResponseDto>.Success(
                        result,
                        "Inquiry submitted successfully. We will contact you soon"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<InquiryResponseDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Get all inquiries
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<IEnumerable<InquiryListDto>>>> GetAll()
        {
            try
            {
                var inquiries = await _inquiryService.GetAllAsync();
                return Ok(ApiResponse<IEnumerable<InquiryListDto>>.Success(
                    inquiries,
                    "Inquiries retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<InquiryListDto>>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Get inquiry by ID
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<InquiryResponseDto>>> GetById(int id)
        {
            try
            {
                var inquiry = await _inquiryService.GetByIdAsync(id);

                if (inquiry == null)
                {
                    return NotFound(ApiResponse<InquiryResponseDto>.Failure(
                        $"Inquiry with ID {id} not found"));
                }

                return Ok(ApiResponse<InquiryResponseDto>.Success(
                    inquiry,
                    "Inquiry retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<InquiryResponseDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Get inquiries by status
        /// </summary>
        [HttpGet("status/{status}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<IEnumerable<InquiryListDto>>>> GetByStatus(
            InquiryStatus status)
        {
            try
            {
                var inquiries = await _inquiryService.GetByStatusAsync(status);
                return Ok(ApiResponse<IEnumerable<InquiryListDto>>.Success(
                    inquiries,
                    $"Inquiries with status '{status}' retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<InquiryListDto>>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Assign inquiry to programmer
        /// </summary>
        [HttpPut("{id}/assign/{programmerId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> AssignToProgrammer(
            int id,
            int programmerId)
        {
            try
            {
                var result = await _inquiryService.AssignToProgrammerAsync(id, programmerId);

                if (!result)
                {
                    return BadRequest(ApiResponse<bool>.Failure(
                        "Failed to assign inquiry to programmer"));
                }

                return Ok(ApiResponse<bool>.Success(
                    true,
                    "Inquiry assigned to programmer successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Update inquiry status
        /// </summary>
        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateStatus(
            int id,
            [FromBody] UpdateInquiryStatusDto request)
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

                var result = await _inquiryService.UpdateStatusAsync(
                    id,
                    request.Status,
                    request.Notes);

                if (!result)
                {
                    return BadRequest(ApiResponse<bool>.Failure(
                        "Failed to update inquiry status"));
                }

                return Ok(ApiResponse<bool>.Success(
                    true,
                    "Inquiry status updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Convert inquiry to customer
        /// </summary>
        [HttpPost("{id}/convert/{customerId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> ConvertToCustomer(
            int id,
            int customerId)
        {
            try
            {
                var result = await _inquiryService.ConvertToCustomerAsync(id, customerId);

                if (!result)
                {
                    return BadRequest(ApiResponse<bool>.Failure(
                        "Failed to convert inquiry to customer"));
                }

                return Ok(ApiResponse<bool>.Success(
                    true,
                    "Inquiry converted to customer successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Get inquiry statistics
        /// </summary>
        [HttpGet("statistics")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<InquiryStatisticsDto>>> GetStatistics()
        {
            try
            {
                var stats = await _inquiryService.GetStatisticsAsync();
                return Ok(ApiResponse<InquiryStatisticsDto>.Success(
                    stats,
                    "Inquiry statistics retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<InquiryStatisticsDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }
    }
}
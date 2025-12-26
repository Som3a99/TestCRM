using CRM.BLL.Common;
using CRM.BLL.DTOs.Requests;
using CRM.BLL.DTOs.Responese;
using CRM.BLL.Interfaces;
using CRM.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<IEnumerable<CustomerDto>>>> GetAll()
        {
            try
            {
                var customers = await _unitOfWork.Customers.GetAllAsync();
                var customerDtos = customers
                    .Where(c => !c.IsDeleted)
                    .Select(c => new CustomerDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        PhoneNumber = c.PhoneNumber,
                        Email = c.Email,
                        CompanyName = c.CompanyName,
                        JoinDate = c.JoinDate
                    }).ToList();

                return Ok(ApiResponse<IEnumerable<CustomerDto>>.Success(
                    customerDtos,
                    "Customers retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<IEnumerable<CustomerDto>>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Get customer by ID
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<CustomerDto>>> GetById(int id)
        {
            try
            {
                var customer = await _unitOfWork.Customers.GetByIdAsync(id);

                if (customer == null || customer.IsDeleted)
                {
                    return NotFound(ApiResponse<CustomerDto>.Failure(
                        $"Customer with ID {id} not found"));
                }

                var customerDto = new CustomerDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    CompanyName = customer.CompanyName,
                    JoinDate = customer.JoinDate
                };

                return Ok(ApiResponse<CustomerDto>.Success(
                    customerDto,
                    "Customer retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<CustomerDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Get customer with projects
        /// </summary>
        [HttpGet("{id}/projects")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<CustomerWithProjectsDto>>> GetCustomerWithProjects(int id)
        {
            try
            {
                var customer = await _unitOfWork.Customers.GetCustomerWithProjectsAsync(id);

                if (customer == null || customer.IsDeleted)
                {
                    return NotFound(ApiResponse<CustomerWithProjectsDto>.Failure(
                        $"Customer with ID {id} not found"));
                }

                var result = new CustomerWithProjectsDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    CompanyName = customer.CompanyName,
                    JoinDate = customer.JoinDate,
                    AllowShowcase = customer.AllowShowcase,
                    Notes = customer.Notes,
                    Projects = customer.Projects
                        .Where(p => !p.IsDeleted)
                        .Select(p => new ProjectBasicDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            WebsiteUrl = p.WebsiteUrl,
                            SystemType = (int)p.SystemType,
                            IsPublished = p.IsPublished,
                            CompletionDate = p.CompletionDate
                        }).ToList(),
                    Testimonials = customer.Testimonials
                        .Where(t => !t.IsDeleted)
                        .Select(t => new TestimonialBasicDto
                        {
                            Id = t.Id,
                            TestimonialText = t.TestimonialText,
                            Rating = t.Rating,
                            IsPublished = t.IsPublished,
                            CreatedAt = t.CreatedAt
                        }).ToList()
                };

                return Ok(ApiResponse<CustomerWithProjectsDto>.Success(
                    result,
                    "Customer data retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<CustomerWithProjectsDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Create new customer
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<CustomerDto>>> Create(
            [FromBody] CreateCustomerDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponse<CustomerDto>.Failure(errors));
                }

                // Check for duplicate email
                if (!string.IsNullOrEmpty(dto.Email))
                {
                    var existingByEmail = await _unitOfWork.Customers.ExistsByEmailAsync(dto.Email);
                    if (existingByEmail)
                    {
                        return BadRequest(ApiResponse<CustomerDto>.Failure(
                            "Email address is already in use"));
                    }
                }

                // Check for duplicate phone
                var existingByPhone = await _unitOfWork.Customers.ExistsByPhoneAsync(dto.PhoneNumber);
                if (existingByPhone)
                {
                    return BadRequest(ApiResponse<CustomerDto>.Failure(
                        "Phone number is already in use"));
                }

                var customer = new Customer
                {
                    Name = dto.Name,
                    PhoneNumber = dto.PhoneNumber,
                    Email = dto.Email,
                    CompanyName = dto.CompanyName,
                    AllowShowcase = dto.AllowShowcase,
                    Notes = dto.Notes,
                    JoinDate = DateOnly.FromDateTime(DateTime.Now),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false
                };

                await _unitOfWork.Customers.AddAsync(customer);
                await _unitOfWork.SaveChangesAsync();

                var customerDto = new CustomerDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    CompanyName = customer.CompanyName,
                    JoinDate = customer.JoinDate
                };

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = customer.Id },
                    ApiResponse<CustomerDto>.Success(
                        customerDto,
                        "Customer created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<CustomerDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Update customer
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<CustomerDto>>> Update(
            int id,
            [FromBody] CreateCustomerDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponse<CustomerDto>.Failure(errors));
                }

                var customer = await _unitOfWork.Customers.GetByIdAsync(id);
                if (customer == null || customer.IsDeleted)
                {
                    return NotFound(ApiResponse<CustomerDto>.Failure(
                        $"Customer with ID {id} not found"));
                }

                // Check for duplicate email (excluding current customer)
                if (!string.IsNullOrEmpty(dto.Email) && dto.Email != customer.Email)
                {
                    var existingByEmail = await _unitOfWork.Customers.ExistsByEmailAsync(dto.Email);
                    if (existingByEmail)
                    {
                        return BadRequest(ApiResponse<CustomerDto>.Failure(
                            "Email address is already in use"));
                    }
                }

                // Check for duplicate phone (excluding current customer)
                if (dto.PhoneNumber != customer.PhoneNumber)
                {
                    var existingByPhone = await _unitOfWork.Customers.ExistsByPhoneAsync(dto.PhoneNumber);
                    if (existingByPhone)
                    {
                        return BadRequest(ApiResponse<CustomerDto>.Failure(
                            "Phone number is already in use"));
                    }
                }

                customer.Name = dto.Name;
                customer.PhoneNumber = dto.PhoneNumber;
                customer.Email = dto.Email;
                customer.CompanyName = dto.CompanyName;
                customer.AllowShowcase = dto.AllowShowcase;
                customer.Notes = dto.Notes;
                customer.UpdatedAt = DateTime.Now;

                _unitOfWork.Customers.Update(customer);
                await _unitOfWork.SaveChangesAsync();

                var customerDto = new CustomerDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    CompanyName = customer.CompanyName,
                    JoinDate = customer.JoinDate
                };

                return Ok(ApiResponse<CustomerDto>.Success(
                    customerDto,
                    "Customer updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<CustomerDto>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Delete customer (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            try
            {
                var customer = await _unitOfWork.Customers.GetByIdAsync(id);
                if (customer == null || customer.IsDeleted)
                {
                    return NotFound(ApiResponse<bool>.Failure(
                        $"Customer with ID {id} not found"));
                }

                customer.IsDeleted = true;
                customer.UpdatedAt = DateTime.Now;

                _unitOfWork.Customers.Update(customer);
                await _unitOfWork.SaveChangesAsync();

                return Ok(ApiResponse<bool>.Success(true, "Customer deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<bool>.Failure(
                    $"An error occurred: {ex.Message}"));
            }
        }
    }
}

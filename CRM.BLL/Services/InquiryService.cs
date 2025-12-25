using AutoMapper;
using CRM.BLL.DTOs.Requests;
using CRM.BLL.DTOs.Responese;
using CRM.BLL.Exceptions;
using CRM.BLL.Interfaces;
using CRM.BLL.Interfaces.Services;
using CRM.DAL.Models;
using CRM.DAL.Models.Enums;

namespace CRM.BLL.Services
{
    public class InquiryService : IInquiryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InquiryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<InquiryResponseDto> CreateAsync(CreateInquiryDto dto)
        {
            var inquiry = _mapper.Map<Inquiry>(dto);

            await _unitOfWork.Inquiries.AddAsync(inquiry);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<InquiryResponseDto>(inquiry);
        }

        public async Task<InquiryResponseDto?> GetByIdAsync(int id)
        {
            var inquiry = await _unitOfWork.Inquiries.GetByIdAsync(id);
            return inquiry != null ? _mapper.Map<InquiryResponseDto>(inquiry) : null;
        }

        public async Task<IEnumerable<InquiryListDto>> GetAllAsync()
        {
            var inquiries = await _unitOfWork.Inquiries.GetAllAsync();
            return _mapper.Map<IEnumerable<InquiryListDto>>(inquiries);
        }

        public async Task<IEnumerable<InquiryListDto>> GetByStatusAsync(InquiryStatus status)
        {
            var inquiries = await _unitOfWork.Inquiries.GetByStatusAsync(status);
            return _mapper.Map<IEnumerable<InquiryListDto>>(inquiries);
        }

        public async Task<bool> AssignToProgrammerAsync(int inquiryId, int programmerId)
        {
            var inquiry = await _unitOfWork.Inquiries.GetByIdAsync(inquiryId);
            if (inquiry == null)
                throw new NotFoundException(nameof(Inquiry), inquiryId);

            var programmer = await _unitOfWork.Programmers.GetByIdAsync(programmerId);
            if (programmer == null)
                throw new NotFoundException(nameof(Programmer), programmerId);

            inquiry.AssignedToProgrammerId = programmerId;
            inquiry.Status = InquiryStatus.InProgress;

            _unitOfWork.Inquiries.Update(inquiry);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStatusAsync(int inquiryId, InquiryStatus status, string? notes)
        {
            var inquiry = await _unitOfWork.Inquiries.GetByIdAsync(inquiryId);
            if (inquiry == null)
                throw new NotFoundException(nameof(Inquiry), inquiryId);

            inquiry.Status = status;
            inquiry.ResponseNotes = notes;
            inquiry.ResponseDate = DateTime.Now;

            _unitOfWork.Inquiries.Update(inquiry);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> ConvertToCustomerAsync(int inquiryId, int customerId)
        {
            var inquiry = await _unitOfWork.Inquiries.GetByIdAsync(inquiryId);
            if (inquiry == null)
                throw new NotFoundException(nameof(Inquiry), inquiryId);

            var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
            if (customer == null)
                throw new NotFoundException(nameof(Customer), customerId);

            inquiry.ConvertedToCustomerId = customerId;
            inquiry.Status = InquiryStatus.Converted;

            _unitOfWork.Inquiries.Update(inquiry);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<InquiryStatisticsDto> GetStatisticsAsync()
        {
            var statusCounts = await _unitOfWork.Inquiries.GetCountByStatusAsync();
            var total = statusCounts.Values.Sum();
            var converted = statusCounts.GetValueOrDefault(InquiryStatus.Converted, 0);

            return new InquiryStatisticsDto
            {
                TotalInquiries = total,
                NewInquiries = statusCounts.GetValueOrDefault(InquiryStatus.New, 0),
                InProgressInquiries = statusCounts.GetValueOrDefault(InquiryStatus.InProgress, 0),
                ConvertedInquiries = converted,
                ConversionRate = total > 0 ? (converted / (double)total) * 100 : 0,
                InquiriesByStatus = statusCounts.ToDictionary(
                    kvp => kvp.Key.ToString(),
                    kvp => kvp.Value
                )
            };
        }
    }
}

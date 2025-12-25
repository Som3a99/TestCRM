using AutoMapper;
using CRM.BLL.DTOs.Requests;
using CRM.BLL.DTOs.Responese;
using CRM.BLL.Exceptions;
using CRM.BLL.Interfaces;
using CRM.BLL.Interfaces.Services;
using CRM.DAL.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CRM.BLL.Services
{
    public class TestimonialService : ITestimonialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public TestimonialService(IUnitOfWork unitOfWork, IMemoryCache cache, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<TestimonialResponseDto> CreateAsync(CreateTestimonialDto dto)
        {
            var testimonial = _mapper.Map<Testimonial>(dto);

            await _unitOfWork.Testimonials.AddAsync(testimonial);
            await _unitOfWork.SaveChangesAsync();

            _cache.Remove("published_testimonials");

            return _mapper.Map<TestimonialResponseDto>(testimonial);
        }

        public async Task<bool> UpdateAsync(int id, UpdateTestimonialDto dto)
        {
            var testimonial = await _unitOfWork.Testimonials.GetByIdAsync(id);
            if (testimonial == null)
                throw new NotFoundException(nameof(Testimonial), id);

            _mapper.Map(dto, testimonial);

            _unitOfWork.Testimonials.Update(testimonial);

            _cache.Remove("published_testimonials");

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var testimonial = await _unitOfWork.Testimonials.GetByIdAsync(id);
            if (testimonial == null)
                throw new NotFoundException(nameof(Testimonial), id);

            testimonial.IsDeleted = true;
            _unitOfWork.Testimonials.Update(testimonial);

            _cache.Remove("published_testimonials");

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<TestimonialDto>> GetPublishedAsync()
        {
            const string cacheKey = "published_testimonials";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<TestimonialDto>? cached))
            {
                var testimonials = await _unitOfWork.Testimonials.GetPublishedAsync();
                cached = _mapper.Map<IEnumerable<TestimonialDto>>(testimonials);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                _cache.Set(cacheKey, cached, cacheOptions);
            }

            return cached ?? Enumerable.Empty<TestimonialDto>();
        }

        public async Task<bool> PublishAsync(int id)
        {
            var testimonial = await _unitOfWork.Testimonials.GetByIdAsync(id);
            if (testimonial == null)
                throw new NotFoundException(nameof(Testimonial), id);

            testimonial.IsPublished = true;
            _unitOfWork.Testimonials.Update(testimonial);

            _cache.Remove("published_testimonials");

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UnpublishAsync(int id)
        {
            var testimonial = await _unitOfWork.Testimonials.GetByIdAsync(id);
            if (testimonial == null)
                throw new NotFoundException(nameof(Testimonial), id);

            testimonial.IsPublished = false;
            _unitOfWork.Testimonials.Update(testimonial);

            _cache.Remove("published_testimonials");

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}

using AutoMapper;
using CRM.BLL.DTOs.Requests;
using CRM.BLL.DTOs.Responese;
using CRM.DAL.Models;
using CRM.DAL.Models.Enums;

namespace CRM.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateInquiryMaps();
            CreateProjectMaps();
            CreateProgrammerMaps();
            CreateTestimonialMaps();
            CreateCustomerMaps();
            CreateTechnologyMaps();
        }

        private void CreateInquiryMaps()
        {
            // CreateInquiryDto -> Inquiry
            CreateMap<CreateInquiryDto, Inquiry>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.InquiryDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => InquiryStatus.New))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => PriorityLevel.Medium))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.VoiceMessagePath, opt => opt.Ignore())
                .ForMember(dest => dest.ResponseNotes, opt => opt.Ignore())
                .ForMember(dest => dest.ResponseDate, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedToProgrammerId, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedToProgrammer, opt => opt.Ignore())
                .ForMember(dest => dest.ConvertedToCustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.ConvertedToCustomer, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // Inquiry -> InquiryResponseDto
            CreateMap<Inquiry, InquiryResponseDto>()
                .ForMember(dest => dest.StatusNameAr, opt => opt.MapFrom(src => GetInquiryStatusNameAr(src.Status ?? InquiryStatus.New)))
                .ForMember(dest => dest.StatusNameEn, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.PriorityNameAr, opt => opt.MapFrom(src => GetPriorityNameAr(src.Priority ?? PriorityLevel.Medium)))
                .ForMember(dest => dest.PriorityNameEn, opt => opt.MapFrom(src => src.Priority.ToString()));

            // Inquiry -> InquiryListDto
            CreateMap<Inquiry, InquiryListDto>()
                .ForMember(dest => dest.StatusNameAr, opt => opt.MapFrom(src => GetInquiryStatusNameAr(src.Status ?? InquiryStatus.New)))
                .ForMember(dest => dest.StatusNameEn, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.AssignedToProgrammerName, opt => opt.MapFrom(src => src.AssignedToProgrammer != null ? src.AssignedToProgrammer.Name : null));
        }

        private void CreateProjectMaps()
        {
            // CreateProjectDto -> Project
            CreateMap<CreateProjectDto, Project>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ThumbnailImagePath, opt => opt.MapFrom(src => "/uploads/projects/default.jpg"))
                .ForMember(dest => dest.IsPublished, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.ViewCount, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.CompletionDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.ProjectTechnologies, opt => opt.Ignore())
                .ForMember(dest => dest.ProgrammersOfProject, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectResources, opt => opt.Ignore())
                .ForMember(dest => dest.Testimonials, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.CompletionDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectTechnologies, opt => opt.Ignore())
                .ForMember(dest => dest.ProgrammersOfProject, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectResources, opt => opt.Ignore())
                .ForMember(dest => dest.Testimonials, opt => opt.Ignore());

            // UpdateProjectDto -> Project
            CreateMap<UpdateProjectDto, Project>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ThumbnailImagePath, opt => opt.Ignore())
                .ForMember(dest => dest.IsPublished, opt => opt.Ignore())
                .ForMember(dest => dest.ViewCount, opt => opt.Ignore())
                .ForMember(dest => dest.CompletionDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectTechnologies, opt => opt.Ignore())
                .ForMember(dest => dest.ProgrammersOfProject, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectResources, opt => opt.Ignore())
                .ForMember(dest => dest.Testimonials, opt => opt.Ignore());

            // Project -> ProjectResponseDto
            CreateMap<Project, ProjectResponseDto>();

            // Project -> ProjectCardDto
            CreateMap<Project, ProjectCardDto>()
                .ForMember(dest => dest.SystemTypeNameAr, opt => opt.MapFrom(src => GetSystemTypeNameAr(src.SystemType)))
                .ForMember(dest => dest.SystemTypeNameEn, opt => opt.MapFrom(src => src.SystemType.ToString()))
                .ForMember(dest => dest.TechnologyNames, opt => opt.MapFrom(src =>
                    src.ProjectTechnologies.Select(pt => pt.Technology.TechnologyName).ToList()));

            // Project -> ProjectDetailsDto
            CreateMap<Project, ProjectDetailsDto>()
                .ForMember(dest => dest.SystemTypeNameAr, opt => opt.MapFrom(src => GetSystemTypeNameAr(src.SystemType)))
                .ForMember(dest => dest.SystemTypeNameEn, opt => opt.MapFrom(src => src.SystemType.ToString()))
                .ForMember(dest => dest.Technologies, opt => opt.MapFrom(src =>
                    src.ProjectTechnologies.Select(pt => pt.Technology)))
                .ForMember(dest => dest.Programmers, opt => opt.MapFrom(src =>
                    src.ProgrammersOfProject.Select(pp => pp.Programmer)))
                .ForMember(dest => dest.Resources, opt => opt.MapFrom(src => src.ProjectResources))
                .ForMember(dest => dest.Testimonials, opt => opt.MapFrom(src =>
                    src.Testimonials.Where(t => t.IsPublished)));

            // ProjectResource -> ProjectResourceDto
            CreateMap<ProjectResource, ProjectResourceDto>();
        }

        private void CreateProgrammerMaps()
        {
            // CreateProgrammerDto -> Programmer
            CreateMap<CreateProgrammerDto, Programmer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => "/uploads/programmers/default.jpg"))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.ProjectsOfProgrammer, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedInquiries, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectsOfProgrammer, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedInquiries, opt => opt.Ignore());

            // UpdateProgrammerDto -> Programmer
            CreateMap<UpdateProgrammerDto, Programmer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectsOfProgrammer, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedInquiries, opt => opt.Ignore());

            // Programmer -> ProgrammerResponseDto
            CreateMap<Programmer, ProgrammerResponseDto>();

            // Programmer -> ProgrammerCardDto
            CreateMap<Programmer, ProgrammerCardDto>();

            // Programmer -> ProgrammerDetailsDto
            CreateMap<Programmer, ProgrammerDetailsDto>()
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src =>
                    src.ProjectsOfProgrammer
                        .Where(pp => pp.Project.IsPublished)
                        .Select(pp => pp.Project)));
        }

        private void CreateTestimonialMaps()
        {
            // CreateTestimonialDto -> Testimonial
            CreateMap<CreateTestimonialDto, Testimonial>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ClientImagePath, opt => opt.Ignore())
                .ForMember(dest => dest.IsPublished, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore())
                .ForMember(dest => dest.ClientImagePath, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore());

            // UpdateTestimonialDto -> Testimonial
            CreateMap<UpdateTestimonialDto, Testimonial>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ClientImagePath, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectId, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore());

            // Testimonial -> TestimonialResponseDto
            CreateMap<Testimonial, TestimonialResponseDto>();

            // Testimonial -> TestimonialDto
            CreateMap<Testimonial, TestimonialDto>();
        }

        private void CreateCustomerMaps()
        {
            // CreateCustomerDto -> Customer
            CreateMap<CreateCustomerDto, Customer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.JoinDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(DateTime.Now)))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Projects, opt => opt.Ignore())
                .ForMember(dest => dest.Inquiries, opt => opt.Ignore())
                .ForMember(dest => dest.Testimonials, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Projects, opt => opt.Ignore())
                .ForMember(dest => dest.Inquiries, opt => opt.Ignore())
                .ForMember(dest => dest.Testimonials, opt => opt.Ignore());

            // Customer -> CustomerDto
            CreateMap<Customer, CustomerDto>();
        }

        private void CreateTechnologyMaps()
        {
            // CreateTechnologyDto -> Technology
            CreateMap<CreateTechnologyDto, Technology>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IconPath, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.TechnologyOfProjects, opt => opt.Ignore())
                .ForMember(dest => dest.IconPath, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.TechnologyOfProjects, opt => opt.Ignore());

            // Technology -> TechnologyDto
            CreateMap<Technology, TechnologyDto>();
        }


        // ===================================================================
        // HELPER METHODS FOR LOCALIZATION
        // ===================================================================

        private static string GetInquiryStatusNameAr(InquiryStatus status) => status switch
        {
            InquiryStatus.New => "جديد",
            InquiryStatus.InProgress => "قيد المعالجة",
            InquiryStatus.Responded => "تم الرد",
            InquiryStatus.Converted => "تم التحويل",
            InquiryStatus.Rejected => "مرفوض",
            _ => status.ToString()
        };

        private static string GetPriorityNameAr(PriorityLevel priority) => priority switch
        {
            PriorityLevel.Low => "منخفض",
            PriorityLevel.Medium => "متوسط",
            PriorityLevel.High => "عالي",
            PriorityLevel.Urgent => "عاجل",
            _ => priority.ToString()
        };

        private static string GetSystemTypeNameAr(SystemType type) => type switch
        {
            SystemType.ECommerce => "متجر إلكتروني",
            SystemType.ERPSystem => "نظام ERP",
            SystemType.CRMSystem => "نظام CRM",
            SystemType.LandingPage => "صفحة هبوط",
            _ => type.ToString()
        };
    }
}

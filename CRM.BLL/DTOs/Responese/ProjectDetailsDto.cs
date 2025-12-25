using CRM.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.DTOs.Responese
{
    public class ProjectDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string WebsiteUrl { get; set; } = null!;
        public string ThumbnailImagePath { get; set; } = null!;
        public string? DefaultUserName { get; set; }
        public string? DefaultPassword { get; set; }
        public SystemType SystemType { get; set; }
        public string SystemTypeNameAr { get; set; } = null!;
        public string SystemTypeNameEn { get; set; } = null!;
        public DateOnly? CompletionDate { get; set; }
        public string? ProjectDuration { get; set; }
        public string? ClientFeedback { get; set; }
        public int ViewCount { get; set; }
        public List<TechnologyDto> Technologies { get; set; } = new();
        public List<ProgrammerCardDto> Programmers { get; set; } = new();
        public List<ProjectResourceDto> Resources { get; set; } = new();
        public List<TestimonialDto> Testimonials { get; set; } = new();
    }
}

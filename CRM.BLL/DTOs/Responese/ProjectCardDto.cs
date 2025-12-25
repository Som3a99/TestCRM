using CRM.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.DTOs.Responese
{
    public class ProjectCardDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ThumbnailImagePath { get; set; } = null!;
        public string WebsiteUrl { get; set; } = null!;
        public SystemType SystemType { get; set; }
        public string SystemTypeNameAr { get; set; } = null!;
        public string SystemTypeNameEn { get; set; } = null!;
        public string? ProjectDuration { get; set; }
        public int ViewCount { get; set; }
        public List<string> TechnologyNames { get; set; } = new();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.DTOs.Responese
{
    public class ProjectResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string WebsiteUrl { get; set; } = null!;
        public string ThumbnailImagePath { get; set; } = null!;
        public bool IsPublished { get; set; }
    }
}

using CRM.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.DTOs.Responese
{
    public class ProjectResourceDto
    {
        public int Id { get; set; }
        public string ItemPath { get; set; } = null!;
        public ResourceType ResourceType { get; set; }
        public string? Title { get; set; }
        public int DisplayOrder { get; set; }
    }
}

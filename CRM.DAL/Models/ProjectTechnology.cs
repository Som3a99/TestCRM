using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.Models
{
    public class ProjectTechnology
    {
        public int Id { get; set; }

        // Audit Fields
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        #region Navigational Properties
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public int TechnologyId { get; set; }
        public Technology Technology { get; set; } = null!;
        #endregion
    }
}

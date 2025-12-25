using CRM.DAL.Models;
using CRM.DAL.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CRM.DAL
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {
        }

        #region DbSets
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<Programmer> Programmers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectProgrammer> ProjectProgrammers { get; set; }
        public DbSet<ProjectResource> ProjectResources { get; set; }
        public DbSet<ProjectTechnology> ProjectTechnologies { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Service> Services { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Global Query Filters (Soft Delete)

            modelBuilder.Entity<Customer>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Inquiry>().HasQueryFilter(i => !i.IsDeleted);
            modelBuilder.Entity<Programmer>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Project>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<ProjectProgrammer>().HasQueryFilter(pp => !pp.IsDeleted);
            modelBuilder.Entity<ProjectResource>().HasQueryFilter(pr => !pr.IsDeleted);
            modelBuilder.Entity<ProjectTechnology>().HasQueryFilter(pt => !pt.IsDeleted);
            modelBuilder.Entity<Technology>().HasQueryFilter(t => !t.IsDeleted);
            modelBuilder.Entity<Testimonial>().HasQueryFilter(t => !t.IsDeleted);
            modelBuilder.Entity<Service>().HasQueryFilter(s => !s.IsDeleted);

            #endregion

            #region Entity Configurations

            // ============================================
            // CUSTOMER CONFIGURATION
            // ============================================
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(c => c.Email)
                    .HasMaxLength(100);

                entity.Property(c => c.CompanyName)
                    .HasMaxLength(200);

                entity.Property(c => c.Notes)
                    .HasMaxLength(1000);

                entity.Property(c => c.AllowShowcase)
                    .HasDefaultValue(false);

                entity.Property(c => c.IsDeleted)
                    .HasDefaultValue(false);

                entity.Property(c => c.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(c => c.UpdatedAt)
                    .HasDefaultValueSql("GETDATE()");

                // Relationships
                entity.HasMany(c => c.Projects)
                    .WithOne(p => p.Customer)
                    .HasForeignKey(p => p.CustomerId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(c => c.Inquiries)
                    .WithOne(i => i.ConvertedToCustomer)
                    .HasForeignKey(i => i.ConvertedToCustomerId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(c => c.Testimonials)
                    .WithOne(t => t.Customer)
                    .HasForeignKey(t => t.CustomerId)
                    .OnDelete(DeleteBehavior.SetNull);

                // Indexes
                entity.HasIndex(c => c.PhoneNumber);
                entity.HasIndex(c => c.Email);
                entity.HasIndex(c => c.IsDeleted);
            });

            // ============================================
            // INQUIRY CONFIGURATION
            // ============================================
            modelBuilder.Entity<Inquiry>(entity =>
            {
                entity.HasKey(i => i.Id);

                entity.Property(i => i.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(i => i.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(i => i.Email)
                    .HasMaxLength(100);

                entity.Property(i => i.PreferredContactTime)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(i => i.Message)
                    .HasMaxLength(1000);

                entity.Property(i => i.VoiceMessagePath)
                    .HasMaxLength(500);

                entity.Property(i => i.ResponseNotes)
                    .HasMaxLength(1000);

                entity.Property(i => i.AvailableBudget)
                    .HasColumnType("decimal(18,2)");

                entity.Property(i => i.Status)
                    .HasDefaultValue(InquiryStatus.New);

                entity.Property(i => i.Priority)
                    .HasDefaultValue(PriorityLevel.Medium);

                entity.Property(i => i.IsDeleted)
                    .HasDefaultValue(false);

                entity.Property(i => i.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(i => i.UpdatedAt)
                    .HasDefaultValueSql("GETDATE()");

                // Relationships
                entity.HasOne(i => i.AssignedToProgrammer)
                    .WithMany(p => p.AssignedInquiries)
                    .HasForeignKey(i => i.AssignedToProgrammerId)
                    .OnDelete(DeleteBehavior.SetNull);

                // Indexes
                entity.HasIndex(i => i.Status);
                entity.HasIndex(i => i.Priority);
                entity.HasIndex(i => i.InquiryDate);
                entity.HasIndex(i => i.IsDeleted);
            });

            // ============================================
            // PROGRAMMER CONFIGURATION
            // ============================================
            modelBuilder.Entity<Programmer>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.ImagePath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(p => p.Brief)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(p => p.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(p => p.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.LinkedIn)
                    .HasMaxLength(200);

                entity.Property(p => p.Github)
                    .HasMaxLength(200);

                entity.Property(p => p.DisplayOrder)
                    .HasDefaultValue(0);

                entity.Property(p => p.IsActive)
                    .HasDefaultValue(true);

                entity.Property(p => p.IsDeleted)
                    .HasDefaultValue(false);

                entity.Property(p => p.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(p => p.UpdatedAt)
                    .HasDefaultValueSql("GETDATE()");

                // Relationships
                entity.HasMany(p => p.ProjectsOfProgrammer)
                    .WithOne(pp => pp.Programmer)
                    .HasForeignKey(pp => pp.ProgrammerId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Indexes
                entity.HasIndex(p => p.Email).IsUnique();
                entity.HasIndex(p => p.IsActive);
                entity.HasIndex(p => p.DisplayOrder);
                entity.HasIndex(p => p.IsDeleted);
            });

            // ============================================
            // PROJECT CONFIGURATION
            // ============================================
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(p => p.Description)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(p => p.WebsiteUrl)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(p => p.ThumbnailImagePath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(p => p.DefaultUserName)
                    .HasMaxLength(100);

                entity.Property(p => p.DefaultPassword)
                    .HasMaxLength(100);

                entity.Property(p => p.ProjectDuration)
                    .HasMaxLength(50);

                entity.Property(p => p.ClientFeedback)
                    .HasMaxLength(500);

                entity.Property(p => p.DisplayOrder)
                    .HasDefaultValue(0);

                entity.Property(p => p.IsPublished)
                    .HasDefaultValue(false);

                entity.Property(p => p.ViewCount)
                    .HasDefaultValue(0);

                entity.Property(p => p.IsDeleted)
                    .HasDefaultValue(false);

                entity.Property(p => p.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(p => p.UpdatedAt)
                    .HasDefaultValueSql("GETDATE()");

                // Relationships
                entity.HasMany(p => p.ProgrammersOfProject)
                    .WithOne(pp => pp.Project)
                    .HasForeignKey(pp => pp.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(p => p.ProjectTechnologies)
                    .WithOne(pt => pt.Project)
                    .HasForeignKey(pt => pt.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(p => p.ProjectResources)
                    .WithOne(pr => pr.Project)
                    .HasForeignKey(pr => pr.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(p => p.Testimonials)
                    .WithOne(t => t.Project)
                    .HasForeignKey(t => t.ProjectId)
                    .OnDelete(DeleteBehavior.SetNull);

                // Indexes
                entity.HasIndex(p => p.IsPublished);
                entity.HasIndex(p => p.DisplayOrder);
                entity.HasIndex(p => p.SystemType);
                entity.HasIndex(p => p.IsDeleted);
            });

            // ============================================
            // PROJECT PROGRAMMER CONFIGURATION
            // ============================================
            modelBuilder.Entity<ProjectProgrammer>(entity =>
            {
                entity.HasKey(pp => pp.Id);

                entity.Property(pp => pp.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(pp => pp.IsDeleted)
                    .HasDefaultValue(false);

                entity.Property(pp => pp.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(pp => pp.UpdatedAt)
                    .HasDefaultValueSql("GETDATE()");

                // Indexes
                entity.HasIndex(pp => new { pp.ProjectId, pp.ProgrammerId });
                entity.HasIndex(pp => pp.IsDeleted);
            });

            // ============================================
            // PROJECT RESOURCE CONFIGURATION
            // ============================================
            modelBuilder.Entity<ProjectResource>(entity =>
            {
                entity.HasKey(pr => pr.Id);

                entity.Property(pr => pr.ItemPath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(pr => pr.Title)
                    .HasMaxLength(200);

                entity.Property(pr => pr.DisplayOrder)
                    .HasDefaultValue(0);

                entity.Property(pr => pr.IsDeleted)
                    .HasDefaultValue(false);

                entity.Property(pr => pr.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(pr => pr.UpdatedAt)
                    .HasDefaultValueSql("GETDATE()");

                // Indexes
                entity.HasIndex(pr => pr.ProjectId);
                entity.HasIndex(pr => pr.DisplayOrder);
                entity.HasIndex(pr => pr.IsDeleted);
            });

            // ============================================
            // PROJECT TECHNOLOGY CONFIGURATION
            // ============================================
            modelBuilder.Entity<ProjectTechnology>(entity =>
            {
                entity.HasKey(pt => pt.Id);

                entity.Property(pt => pt.IsDeleted)
                    .HasDefaultValue(false);

                entity.Property(pt => pt.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(pt => pt.UpdatedAt)
                    .HasDefaultValueSql("GETDATE()");

                // Unique constraint - prevent duplicate technology for same project
                entity.HasIndex(pt => new { pt.ProjectId, pt.TechnologyId })
                    .IsUnique();

                entity.HasIndex(pt => pt.IsDeleted);
            });

            // ============================================
            // TECHNOLOGY CONFIGURATION
            // ============================================
            modelBuilder.Entity<Technology>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.TechnologyName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(t => t.IconPath)
                    .HasMaxLength(500);

                entity.Property(t => t.Category)
                    .HasMaxLength(50);

                entity.Property(t => t.DisplayOrder)
                    .HasDefaultValue(0);

                entity.Property(t => t.IsActive)
                    .HasDefaultValue(true);

                entity.Property(t => t.IsDeleted)
                    .HasDefaultValue(false);

                entity.Property(t => t.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(t => t.UpdatedAt)
                    .HasDefaultValueSql("GETDATE()");

                // Relationships
                entity.HasMany(t => t.TechnologyOfProjects)
                    .WithOne(pt => pt.Technology)
                    .HasForeignKey(pt => pt.TechnologyId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Indexes
                entity.HasIndex(t => t.TechnologyName).IsUnique();
                entity.HasIndex(t => t.Category);
                entity.HasIndex(t => t.IsActive);
                entity.HasIndex(t => t.DisplayOrder);
                entity.HasIndex(t => t.IsDeleted);
            });

            // ============================================
            // TESTIMONIAL CONFIGURATION
            // ============================================
            modelBuilder.Entity<Testimonial>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.ClientName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(t => t.ClientCompany)
                    .HasMaxLength(200);

                entity.Property(t => t.ClientPosition)
                    .HasMaxLength(100);

                entity.Property(t => t.TestimonialText)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(t => t.ClientImagePath)
                    .HasMaxLength(500);

                entity.Property(t => t.DisplayOrder)
                    .HasDefaultValue(0);

                entity.Property(t => t.IsPublished)
                    .HasDefaultValue(false);

                entity.Property(t => t.IsDeleted)
                    .HasDefaultValue(false);

                entity.Property(t => t.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(t => t.UpdatedAt)
                    .HasDefaultValueSql("GETDATE()");

                // Indexes
                entity.HasIndex(t => t.IsPublished);
                entity.HasIndex(t => t.DisplayOrder);
                entity.HasIndex(t => t.Rating);
                entity.HasIndex(t => t.IsDeleted);
            });

            // ============================================
            // SERVICE CONFIGURATION
            // ============================================
            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.ServiceName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(s => s.Description)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(s => s.ShortDescription)
                    .HasMaxLength(500);

                entity.Property(s => s.IconPath)
                    .HasMaxLength(500);

                entity.Property(s => s.DisplayOrder)
                    .HasDefaultValue(0);

                entity.Property(s => s.IsActive)
                    .HasDefaultValue(true);

                entity.Property(s => s.IsDeleted)
                    .HasDefaultValue(false);

                entity.Property(s => s.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(s => s.UpdatedAt)
                    .HasDefaultValueSql("GETDATE()");

                // Indexes
                entity.HasIndex(s => s.IsActive);
                entity.HasIndex(s => s.DisplayOrder);
                entity.HasIndex(s => s.IsDeleted);
            });

            #endregion

            #region Seed Data

            // Seed Technologies
            modelBuilder.Entity<Technology>().HasData(
                new Technology
                {
                    Id = 1,
                    TechnologyName = "C#",
                    Category = "Backend",
                    DisplayOrder = 1,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Technology
                {
                    Id = 2,
                    TechnologyName = "ASP.NET Core",
                    Category = "Backend",
                    DisplayOrder = 2,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Technology
                {
                    Id = 3,
                    TechnologyName = "React",
                    Category = "Frontend",
                    DisplayOrder = 3,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Technology
                {
                    Id = 4,
                    TechnologyName = "SQL Server",
                    Category = "Database",
                    DisplayOrder = 4,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Technology
                {
                    Id = 5,
                    TechnologyName = "Entity Framework",
                    Category = "ORM",
                    DisplayOrder = 5,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );

            #endregion
        }

        
        //public override int SaveChanges()
        //{
        //    UpdateTimestamps();
        //    return base.SaveChanges();
        //}

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{

        //    UpdateTimestamps();
        //    return base.SaveChangesAsync(cancellationToken);
        //}

        //private void UpdateTimestamps()
        //{
        //    var entries = ChangeTracker.Entries()
        //        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        //    foreach (var entry in entries)
        //    {
        //        if (entry.State == EntityState.Added)
        //        {
        //            var createdAtProp = entry.Property("CreatedAt");
        //            if (createdAtProp.CurrentValue == null ||
        //                createdAtProp.CurrentValue is DateTime dt && dt == default)
        //            {
        //                createdAtProp.CurrentValue = DateTime.Now;
        //            }
        //        }

        //        var updatedAtProp = entry.Property("UpdatedAt");
        //        if (updatedAtProp != null)
        //        {
        //            updatedAtProp.CurrentValue = DateTime.Now;
        //        }
        //    }
        //}
    }
}


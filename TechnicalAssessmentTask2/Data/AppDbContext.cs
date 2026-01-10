using Microsoft.EntityFrameworkCore;
using TechnicalAssessmentTask2.Models;

namespace TechnicalAssessmentTask2.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<SurveyResponse> SurveyResponses => Set<SurveyResponse>();
        public DbSet<TextAnalysis> TextAnalyses => Set<TextAnalysis>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
            });

            // Configure SurveyResponse entity
            modelBuilder.Entity<SurveyResponse>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.ToTable("SurveyResponses");
                entity.Property(e => e.EmployeeId).HasMaxLength(50);
                entity.Property(e => e.Department).HasMaxLength(100);
                entity.Property(e => e.Gender).HasMaxLength(50);
                entity.Property(e => e.Location).HasMaxLength(100);
                entity.Property(e => e.Manager).HasMaxLength(100);
                entity.Property(e => e.LeadershipLevel).HasMaxLength(100);
                entity.Property(e => e.Tenure).HasColumnType("decimal(18,1)");
            });

            // Configure TextAnalysis entity
            modelBuilder.Entity<TextAnalysis>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.ToTable("TextAnalyses");
                entity.Property(e => e.OpenEndedStart1).HasColumnType("nvarchar(max)");
                entity.Property(e => e.OpenEndedContinue1).HasColumnType("nvarchar(max)");
                entity.Property(e => e.OpenEndedStop1).HasColumnType("nvarchar(max)");
                entity.Property(e => e.OpenEndedAnythingElse1).HasColumnType("nvarchar(max)");

                //Configure one-to-one relationship with SurveyResponse
                entity.HasOne(e => e.SurveyResponse)
                    .WithOne()
                    .HasForeignKey<TextAnalysis>(e => e.SurveyID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

           
        }
    }
}

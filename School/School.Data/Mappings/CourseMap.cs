using School.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace School.Data.Mappings
{
    public class CourseMap : EntityTypeConfiguration<Course>
    {
        public CourseMap()
        {
            ToTable("course");

            HasKey(c => c.Id);

            Property(c => c.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.CreatedById).HasColumnName("created_by_id").IsRequired();
            Property(c => c.UpdatedById).HasColumnName("updated_by_id").IsRequired();
            Property(c => c.CreatedAt).HasColumnName("created_at");
            Property(c => c.UpdatedAt).HasColumnName("updated_at").IsRequired();
            Property(c => c.Status).HasColumnName("status");
            Property(c => c.Name).HasColumnName("name").HasMaxLength(50).IsRequired();

            HasRequired(c => c.CreatedBy)
                .WithMany(u => u.CoursesCreated)
                .HasForeignKey(c => c.CreatedById)
                .WillCascadeOnDelete(false);

            HasRequired(c => c.UpdatedBy)
                .WithMany(u => u.CoursesUpdated)
                .HasForeignKey(c => c.UpdatedById)
                .WillCascadeOnDelete(false);
        }
    }
}

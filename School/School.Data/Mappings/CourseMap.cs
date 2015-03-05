using School.Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace School.Data.Mappings
{
    public class CourseMap : EntityTypeConfiguration<Course>
    {
        public CourseMap()
        {
            ToTable("course");

            HasKey(c => c.Id);

            Property(c => c.Id).HasColumnName("id");
            Property(c => c.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        }
    }
}

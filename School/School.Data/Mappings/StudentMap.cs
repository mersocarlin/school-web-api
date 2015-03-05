using School.Domain;
using System.Data.Entity.ModelConfiguration;

namespace School.Data.Mappings
{
    public class StudentMap : EntityTypeConfiguration<Student>
    {
        public StudentMap()
        {
            ToTable("student");

            HasKey(s => s.Id);

            Property(s => s.Id).HasColumnName("id");
            Property(s => s.Height).HasColumnName("height").IsRequired();
            Property(s => s.PersonId).HasColumnName("personid");
            
            HasRequired(s => s.Person);
        }
    }
}

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

            Property(s => s.Id).HasColumnName("is");
            Property(s => s.Height).HasColumnName("height").IsRequired();
            Property(s => s.Row).HasColumnName("row_number").IsRequired();
            Property(s => s.Column).HasColumnName("column_number").IsRequired();
            Property(s => s.PersonId).HasColumnName("personid");
            Property(s => s.ClassroomId).HasColumnName("classroomid"); ;

            HasRequired(s => s.Person);
            HasOptional(s => s.Classroom);
        }
    }
}

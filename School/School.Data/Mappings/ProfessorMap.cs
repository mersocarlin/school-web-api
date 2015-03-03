using School.Domain;
using System.Data.Entity.ModelConfiguration;

namespace School.Data.Mappings
{
    public class ProfessorMap : EntityTypeConfiguration<Professor>
    {
        public ProfessorMap()
        {
            ToTable("professor");

            HasKey(p => p.Id);

            Property(p => p.Id).HasColumnName("id");
            Property(p => p.PersonId).HasColumnName("personid");

            HasRequired(p => p.Person);
        }
    }
}

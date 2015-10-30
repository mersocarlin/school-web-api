using School.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace School.Data.Mappings
{
    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            ToTable("person");

            HasKey(p => p.Id);

            Property(p => p.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.CreatedById).HasColumnName("created_by_id").IsRequired();
            Property(p => p.UpdatedById).HasColumnName("updated_by_id").IsRequired();
            Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
            Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();
            Property(p => p.Status).HasColumnName("status").IsRequired();
            Property(p => p.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            Property(p => p.Gender).HasColumnName("gender").IsRequired();
            Property(p => p.DateOfBirth).HasColumnName("date_of_birth").IsRequired();
            Property(p => p.Email).HasColumnName("email").HasMaxLength(50);
            Property(p => p.PersonType).HasColumnName("person_type");
            Property(p => p.HomePhone).HasColumnName("home_phone").HasMaxLength(15);
            Property(p => p.MobilePhone).HasColumnName("mobile_phone").HasMaxLength(15);
            Property(p => p.WorkPhone).HasColumnName("work_phone").HasMaxLength(15);
            Property(p => p.Address).HasColumnName("address");

            HasRequired(p => p.CreatedBy)
                .WithMany(u => u.PersonsCreated)
                .HasForeignKey(p => p.CreatedById)
                .WillCascadeOnDelete(false);

            HasRequired(p => p.UpdatedBy)
                .WithMany(u => u.PersonsUpdated)
                .HasForeignKey(p => p.UpdatedById)
                .WillCascadeOnDelete(false);
        }
    }
}

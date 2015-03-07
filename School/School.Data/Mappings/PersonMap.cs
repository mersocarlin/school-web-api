using School.Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace School.Data.Mappings
{
    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            ToTable("person");

            HasKey(p => p.Id);
            //Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Id).HasColumnName("id");
            Property(p => p.CreatedDate).HasColumnName("created_date").IsRequired();
            Property(p => p.Status).HasColumnName("status").IsRequired();
            Property(p => p.FirstName).HasColumnName("firstname").HasMaxLength(20).IsRequired();
            Property(p => p.LastName).HasColumnName("lastname").HasMaxLength(20).IsRequired();
            Property(p => p.Gender).HasColumnName("gender").IsRequired();
            Property(p => p.DateOfBirth).HasColumnName("dateofbirth").IsRequired();
            Property(p => p.Email).HasColumnName("email").HasMaxLength(30).IsRequired();
            Property(p => p.Password).HasColumnName("password").HasMaxLength(50).IsRequired();
            Property(p => p.HomePhone).HasColumnName("homephone").HasMaxLength(20).IsRequired();
            Property(p => p.MobilePhone).HasColumnName("mobilephone").HasMaxLength(20).IsRequired();
            Property(p => p.WorkPhone).HasColumnName("workphone").HasMaxLength(20).IsRequired();
            Property(p => p.Address).HasColumnName("address").HasMaxLength(100);
            Property(p => p.PersonType).HasColumnName("persontype").IsRequired();
        }
    }
}

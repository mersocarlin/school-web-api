using School.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace School.Data.Mappings
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("user");

            HasKey(u => u.Id);

            Property(u => u.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(u => u.Profile).HasColumnName("profile").IsRequired();
            Property(u => u.Username).HasColumnName("username").HasMaxLength(100).IsRequired();
            Property(u => u.Password).HasColumnName("password").HasMaxLength(100).IsRequired();
            Property(u => u.PersonId).HasColumnName("person_id");
            Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
            Property(u => u.UpdatedAt).HasColumnName("updated_at").IsRequired();
            Property(u => u.LastLogin).HasColumnName("last_login");
            Property(u => u.Status).HasColumnName("status").IsRequired();
            Property(u => u.RefreshTokenId).HasColumnName("refresh_token_id");
            Property(u => u.ProtectedTicket).HasColumnName("protected_ticket");

            HasOptional(u => u.Person);
        }
    }
}

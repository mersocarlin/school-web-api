using School.Data.Mappings;
using School.Domain.Models;
using System.Data.Entity;

namespace School.Data.DataContexts
{
    public class SchoolContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Person> People { get; set; }

        public SchoolContext()
            : base("SchoolContext")
        {
            //Database.SetInitializer<SchoolContext>(null);
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CourseMap());
            modelBuilder.Configurations.Add(new PersonMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}

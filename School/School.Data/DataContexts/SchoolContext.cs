using School.Data.Mappings;
using School.Domain;
using System.Data.Entity;

namespace School.Data.DataContexts
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
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
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new StudentMap());
            modelBuilder.Configurations.Add(new ProfessorMap());
            modelBuilder.Configurations.Add(new ClassroomMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}


namespace School.Domain
{
    public class Professor
    {
        public int Id { get; set; }
        public int PersonId { get; set; }

        public virtual Person Person { get; set; }

        public Professor()
        {

        }
    }
}

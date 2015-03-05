using System;

namespace School.Domain
{
    public class Student
    {
        public int Id { get; set; }
        public int Height { get; set; }
        public int PersonId { get; set; }
        
        public virtual Person Person { get; set; }
        
        public Student()
        {
            
        }

        public override string ToString()
        {
            return String.Format("{0} ({1})",
                    this.Person == null ? this.Id.ToString() : this.Person.FullName,
                    this.Height);
        }
    }
}

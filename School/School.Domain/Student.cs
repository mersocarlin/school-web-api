using System;

namespace School.Domain
{
    public class Student
    {
        public int Id { get; set; }
        public int Height { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int PersonId { get; set; }
        public int ClassroomId { get; set; }

        public virtual Person Person { get; set; }
        public virtual Classroom Classroom { get; set; }

        public Student()
        {
            this.Row = -1;
            this.Column = -1;
        }

        public override string ToString()
        {
            return String.Format("{0} ({1})",
                    this.Person == null ? this.Id.ToString() : this.Person.FullName,
                    this.Height);
        }
    }
}

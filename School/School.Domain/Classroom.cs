using System.Collections.Generic;

namespace School.Domain
{
    public class Classroom
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public Classroom()
        {
            this.Students = new List<Student>();
        }

        public override string ToString()
        {
            return string.Format("{0}\nTotal seats available: {1}\nRowsxColumns: {2}x{3}\n",
                this.Title,
                this.Rows * this.Columns,
                this.Rows,
                this.Columns);
        }
    }
}

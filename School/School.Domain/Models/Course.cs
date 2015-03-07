using DDDValidation.Validation;
using System.Collections.Generic;

namespace School.Domain.Models
{
    public class Course
    {
        #region Ctor
        public Course()
        {
            //this.Students = new List<Person>();
            //this.Teachers = new List<Person>();
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        
        //public virtual ICollection<Person> Students { get; set; }
        //public virtual ICollection<Person> Teachers { get; set; }
        #endregion

        #region Methods
        public void Validate()
        {
            AssertionConcern.AssertArgumentNotNull(this.Name, "Please inform the name of the course");
        }
        #endregion
    }
}

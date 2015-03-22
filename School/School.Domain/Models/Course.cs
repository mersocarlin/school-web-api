using DDDValidation.Validation;
using System.Collections.Generic;
using System;

namespace School.Domain.Models
{
    public class Course
    {
        #region Ctor
        public Course()
        {
            this.CreatedDate = DateTime.Now;
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public EntityStatus Status { get; set; }
        
        #endregion

        #region Methods
        public void Validate()
        {
            AssertionConcern.AssertArgumentNotNull(this.Name, "Please inform the name of the course");
        }
        #endregion
    }
}

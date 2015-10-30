using DDDValidation.Validation;
using System;

namespace School.Domain.Models
{
    public class Course
    {
        #region Ctor
        public Course()
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.Status = EntityStatus.Active;
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public int CreatedById { get; set; }
        public int UpdatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public EntityStatus Status { get; set; }
        public string Name { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User UpdatedBy { get; set; }
        #endregion

        #region Methods
        public void Validate()
        {
            AssertionConcern.AssertArgumentNotNull(this.Name, "Please inform the name of the course");
        }

        public object ToJson()
        {
            return new
            {
                Id = this.Id,
                Name = this.Name
            };
        }
        #endregion
    }
}

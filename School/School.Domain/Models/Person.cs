using DDDValidation.Validation;
using System;

namespace School.Domain.Models
{
    public enum PersonType
    {
        Student = 0,
        Teacher = 1,
        StudentAndTeacher = 2
    }

    public enum GenderType
    {
        Female = 0,
        Male = 1
    }

    public enum EntityStatus
    {
        Inactive = 0,
        Active = 1
    }

    public class Person
    {
        #region ctor
        public Person()
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
        public GenderType Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public PersonType PersonType { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Address { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User UpdatedBy { get; set; }
        #endregion

        #region Methods
        public bool IsTeacher()
        {
            return this.PersonType == Models.PersonType.Teacher || this.PersonType == Models.PersonType.StudentAndTeacher;
        }

        public bool IsStudent()
        {
            return this.PersonType == Models.PersonType.Student || this.PersonType == Models.PersonType.StudentAndTeacher;
        }
        public void Validate()
        {
            AssertionConcern.AssertArgumentNotNull(this.Name, "Please enter the Name");
            AssertionConcern.AssertArgumentLength(this.Name, 100, "Name must be lest than 100 characters");

            if (!string.IsNullOrEmpty(this.Email))
            {
                EmailAssertionConcern.AssertEmailFormat(this.Email, "Please inform a valid email address");
            }

            if (!string.IsNullOrEmpty(this.HomePhone))
            {
                AssertionConcern.AssertArgumentLength(this.HomePhone, 15, "HomePhone must be lest than 15 characters");
            }

            if (!string.IsNullOrEmpty(this.MobilePhone))
            {
                AssertionConcern.AssertArgumentLength(this.MobilePhone, 15, "MobilePhone must be lest than 15 characters");
            }

            if (!string.IsNullOrEmpty(this.WorkPhone))
            {
                AssertionConcern.AssertArgumentLength(this.WorkPhone, 15, "MobilePhone must be lest than 15 characters");
            }

            if (!string.IsNullOrEmpty(this.Address))
            {
                AssertionConcern.AssertArgumentLength(this.Address, 200, "AddressLine1 must be lest than 200 characters");
            }
        }

        public object ToJson()
        {
            return new
            {
                Id = this.Id,
                CreatedAt = this.CreatedAt,
                Name = this.Name,
                Gender = this.Gender,
                DateOfBirth = this.DateOfBirth,
                Email = this.Email,
                PersonType = this.PersonType,
                HomePhone = this.HomePhone,
                MobilePhone = this.MobilePhone,
                WorkPhone = this.WorkPhone,
                Address = this.Address
            };
        }
        #endregion
    }
}

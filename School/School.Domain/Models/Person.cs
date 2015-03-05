﻿using System;

namespace School.Domain.Models
{
    public enum PersonType
    {
        Student = 0,
        Teacher = 1
    }

    public enum GenderType
    {
        Male = 0,
        Female = 1
    }

    public enum PersonStatus
    {
        Active = 1,
        Inactive = 2
    }

    public class Person
    {
        #region ctor
        public Person()
        {
            this.CreatedDate = DateTime.Now;
        }
        #endregion

        #region Properties
        /*
          * CPF, RG, Contact phone (Home, Comercial, Mobile)
         */
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public PersonStatus Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        
        public string Address { get; set; }
        public PersonType PersonType { get; set; }

        public string FullName { get { return this.FirstName + " " + this.LastName; } }
        #endregion

    }
}

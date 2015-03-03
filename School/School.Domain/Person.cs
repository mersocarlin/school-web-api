using System;

namespace School.Domain
{
    public enum PersonType
    {
        Professor = 0,
        Student = 1
    }

    public class Person
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        /// <summary>
        /// 0 - Professor
        /// 1 - Student
        /// </summary>
        public PersonType PersonType { get; set; }


        public string FullName { get { return this.FirstName + " " + this.LastName; } }
    }
}

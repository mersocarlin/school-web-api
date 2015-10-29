using DDDValidation.Validation;
using System;
using System.Collections.Generic;

namespace School.Domain.Models
{
    public enum TokenType
    {
        AccessToken,
        RefreshToken
    }

    public enum UserProfile
    {
        SuperUser = 0,
        Admin = 1,
        Teacher = 2,
        Student = 3
    }

    public class User
    {
        #region Ctor
        public User()
        {
            this.Profile = UserProfile.Student;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.Status = EntityStatus.Active;

            this.CoursesCreated = new List<Course>();
            this.CoursesUpdated = new List<Course>();
            this.PersonsCreated = new List<Person>();
            this.PersonsUpdated = new List<Person>();
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public UserProfile Profile { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? PersonId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public EntityStatus Status { get; set; }
        public string RefreshTokenId { get; set; }
        public string ProtectedTicket { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<Course> CoursesCreated { get; set; }
        public virtual ICollection<Course> CoursesUpdated { get; set; }
        public virtual ICollection<Person> PersonsCreated { get; set; }
        public virtual ICollection<Person> PersonsUpdated { get; set; }
        #endregion

        #region Methods
        public void Validate()
        {
            AssertionConcern.AssertArgumentNotNull(this.Username, "Please enter the Username");
            AssertionConcern.AssertArgumentLength(this.Username, 100, "Username must be lest than 100 characters");

            AssertionConcern.AssertArgumentNotNull(this.Password, "Please enter the Password");
            AssertionConcern.AssertArgumentLength(this.Password, 255, "Password must be lest than 255 characters");
        }

        public object ToJson()
        {
            return new
            {
                Id = this.Id,
                Profile = this.Profile,
                Username = this.Username,
                CreatedAt = this.CreatedAt,
                LastLogin = this.LastLogin,
                Status = this.Status
            };
        }

        public string ToLoginJson()
        {
            return string.Format("{{ \"id\": \"{0}\", \"username\": \"{1}\", \"createdAt\": \"{2}\", \"lastLogin\": \"{3}\", \"profile\": \"{4}\" }}",
                this.Id,
                this.Username,
                this.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.000"),
                this.LastLogin.HasValue ? this.LastLogin.Value.ToString("yyyy-MM-ddTHH:mm:ss.000") : "",
                (int)this.Profile);
        }

        public static string EncryptPassword(string password)
        {
            password += "|DCB5C88E-28F2-4341-BA53-F31C0607FE4C";

            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] data = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(password));

            System.Text.StringBuilder sbString = new System.Text.StringBuilder();

            for (int i = 0; i < data.Length; i++)
                sbString.Append(data[i].ToString("x2"));

            return sbString.ToString();
        }
        #endregion
    }
}

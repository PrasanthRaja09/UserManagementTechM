using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        [Column("id")]
        public int UserID { get; set; }
        [Column("username")]
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        [Column("fname")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Column("lname")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Column("Email")]
        [Required(ErrorMessage = "Email ID is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Enter Valid Email ID")]
        public string EmailID { get; set; }
        [Column("dob")]
        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime DateOfBirth { get; set; }
        [Column("age")]
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
        [Column("Phone")]
        [Required(ErrorMessage = "Phone is required")]
        public string PhoneNumber { get; set; }
        [Column("active")]
        [Required(ErrorMessage = "Status is required")]
        public bool Active { get; set; }
        [Column("createdon")]
        public DateTime? CreatedOn { get; set; }
        [Column("lastmodifiedon")]
        public DateTime? LastModifiedOn { get; set; }

    }
}

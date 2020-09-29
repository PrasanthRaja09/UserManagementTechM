using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("User_Roles")]
    public class User_Roles
    {
        [Key]
        [Column("User_id")]
        [Required(ErrorMessage = "User ID is required")]
        public int UserID { get; set; }
        [Column("Role_id")]
        [Required(ErrorMessage = "Role ID is required")]
        public int RoleID { get; set; }
    }
}

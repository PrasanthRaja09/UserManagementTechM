using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Model
{
    [Table("Role_Permissions")]
    public class RolePermissions
    {
        [Key]
        [Column("id")]
        public int id { get; set; }
        [Column("permission")]
        public string permission { get; set; }
        [Key]
        [Column("rid")]
        public int rid { get; set; }
    }
}

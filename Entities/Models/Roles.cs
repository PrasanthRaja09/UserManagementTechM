using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Model
{
    [Table("Roles")]
    public class Roles
    {
        [Key]
        [Column("id")]
        public int id { get; set; }
        [Column("role")]
        public string role { get; set; }
        [Column("createdon")]
        public DateTime? createdon { get; set; }
        [Column("lastmodifiedon")]
        public DateTime? lastmodifiedon { get; set; }
    }
}

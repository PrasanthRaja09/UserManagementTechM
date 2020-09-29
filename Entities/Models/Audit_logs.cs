using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Model
{
    [Table("Audit_logs")]
    public class Audit_logs
    {
        [Key]
        [Column("id")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Key]
        [Column("uid")]
        public int uid { get; set; }
        [Column("action")]
        public string action { get; set; }
        [Column("log")]
        public string log { get; set; }
        [Column("tablename")]
        public string tablename { get; set; }
        [Column("datetime")]
        public DateTime? datetime { get; set; }
    }
}

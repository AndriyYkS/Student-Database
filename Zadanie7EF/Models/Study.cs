namespace Zadanie7EF.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("apbd.Studies")]
    public partial class Study
    {
        [Key]
        public int IdStudies { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}

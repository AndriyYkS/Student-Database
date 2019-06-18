namespace Zadanie7EF.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("apbd.Student_Subject")]
    public partial class Student_Subject
    {
        [Key]
        [Column(Order = 0)]
        public int IdStudentSubject { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdStudent { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdSubject { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

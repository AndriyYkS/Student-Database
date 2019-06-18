namespace Zadanie7EF.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PjatkDB : DbContext
    {
        public PjatkDB()
            : base("name=PjatkDB")
        {
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Student_Subject> Student_Subject { get; set; }
        public virtual DbSet<Study> Studies { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

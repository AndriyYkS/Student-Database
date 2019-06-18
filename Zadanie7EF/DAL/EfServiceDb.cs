using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadanie7EF.Models;

namespace Zadanie7EF.DAL
{
    class EfServiceDb
    {
        public PjatkDB context;
        public EfServiceDb()
        {
            context = new PjatkDB();
            context.Configuration.LazyLoadingEnabled = false;
        }


        public ICollection<Student> GetStudents()
        {
            var list = new List<Student>();
            try
            {
                var students = context.Students
                                .Include("Study")
                                .ToList();
                return students;
            }
            catch (Exception)
            {
                MessageBox.Show("Error connecting to the database");
                return list;
            }
        }

        public ICollection<Study> GetStudies(){

            var list = new List<Study>();
            try {
                var studies = context.Studies.ToList();

                return studies;
            }
            catch (Exception) {
                MessageBox.Show("Error connecting to the database");
                return list;
            }
        }
        public ICollection<Subject> GetSubjects() {
            var list = new List<Subject>();
            try {
                var subjects = context.Subjects.ToList();

                return subjects;
            }
            catch (Exception) {
                MessageBox.Show("Error connecting to the database");
                return list;
            }
        }
        public void AddStudentToDB(Student student) {
            //if(student.Address == null) { 
             //   student.Address = "adress";
              //}
            if (student.IdStudies == 0) student.IdStudies = 1;
            context.Students.Add(student);
            Commit();
        }
        public void RemoveStudentFromDB(Student toRemove) {
            var student = new Student { IdStudent = toRemove.IdStudent };
          
            //  context.Entry(stu).State = System.Data.Entity.EntityState.Modified;
            var entry = context.Entry(student);
          /*  if (entry.State == EntityState.Detached || entry.State == EntityState.Modified)
            {
                 context.Entry(stu).State = System.Data.Entity.EntityState.Detached;
                entry.State = EntityState.Modified;
               */
                context.Students.Attach(student);
                context.Students.Remove(student);
                context.SaveChanges();
        }

        public void Commit() {
            bool saveFailed;
            do {
                saveFailed = false;
                try {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex) {
                    saveFailed = true;
 
                    ex.Entries.Single().Reload();
                }
            } while (saveFailed);

        }
    }
}

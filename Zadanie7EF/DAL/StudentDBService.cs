using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadanie7EF.Models;

namespace Zadanie7EF.DAL
{
    class StudentDBService
    {
        protected ObservableCollection<Student> studentsList;
        String connectionString = "Data Source=SQLEXPRESS;Initial Catalog=master;Integrated Security=True";

        public StudentDBService()
        { /*
            studentsList = new ObservableCollection<Student>();

            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString)
               )
                {
                    connect.Open();

                    using (SqlCommand command = new SqlCommand("select * from apbd.Student", connect))
                    using (SqlDataReader reader = command.ExecuteReader())
                        while (reader.Read())
                        {
                            studentsList.Add(new Student
                            {
                                idStudent = Convert.ToInt32(reader[0]),
                                //8
                                imie = reader[1].ToString(),
                                nazwisko = reader[2].ToString(),
                                adress = reader[3].ToString(),
                                nrindeksu = reader[4].ToString(),
                                idstudia = reader[5].ToString()
                            });
                        }

                    //pobieramy przedmioty
                    foreach (Student st in studentsList)
                        if (st.idStudent != 0)
                            st.wybranePrzedmioty = getStudentSubject(Convert.ToInt32(st.idStudent));
                }
            }
            catch (Exception e)
            { //9
                Console.Write("DataBase connection Exception");
            }
        }

        private List<string> getStudentSubject(int idst)
        {
            List<String> listOfSubjects = new List<String>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("select sb.Name from apbd.Subject sb,apbd.Student_Subject ss where" +
                        " ss.IdStudent=@idstud and ss.IdSubject=sb.IdSubject", connection))
                    {
                        command.Parameters.AddWithValue("@idstud", idst);

                        using (SqlDataReader reader = command.ExecuteReader())
                            while (reader.Read())
                                listOfSubjects.Add(reader[0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return listOfSubjects;
        }

        public ObservableCollection<Student> GetStudentList()
        {
            return studentsList;
        }

        public void removeStudent(Student deleted)
        {
            studentsList.Remove(deleted);
            //another style
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM apbd.Student WHERE idStudent=@id", connection))
                {
                    command.Parameters.AddWithValue("@id", deleted.idStudent);
                    int affectedRows = command.ExecuteNonQuery();
                    Console.Write(affectedRows);
                }
            }
        }

        internal void addStudentSubjects(int studId, String subjectName)
        {
            int subjectId = 0;

            //najpierw wyciagnac subjectID
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("select IdSubject from apbd.Subject where Name=@subjectName", connection))
                {
                    command.Parameters.AddWithValue("@subjectName", subjectName);

                    using (SqlDataReader reader = command.ExecuteReader())
                        while (reader.Read())
                            subjectId = Convert.ToInt32(reader[0]);
                }
                Console.WriteLine();
                Console.WriteLine(subjectId + "<--------");
                //dodajemy przedmioty studenta do tabeli 

                using (SqlCommand command = new SqlCommand("INSERT INTO apbd.Student_Subject(IdStudent,IdSubject,CreatedAt) " +
                    "values(@idstudent,@idsubject,@creat)", connection))
                {
                    command.Parameters.AddWithValue("idstudent", studId);
                    command.Parameters.AddWithValue("idsubject", subjectId);
                    command.Parameters.AddWithValue("creat", DateTime.Now);
                    int affectedRows = command.ExecuteNonQuery();
                    Console.Write(affectedRows);
                }

            }
        }

        public List<String> getStudiesList()
        {
            List<String> sqlresult = new List<String>();

            //   var queryAllCustomers = from wrt in Studies
            //                        select Name;

            using (SqlConnection connect = new SqlConnection(connectionString)
          )
            {
                connect.Open();

                using (SqlCommand command = new SqlCommand("select * from apbd.Studies", connect))
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        sqlresult.Add(reader[1].ToString());
                    }
            }
            return sqlresult;
        }

        public List<String> getSubjectList()
        {
            List<String> sqlresult = new List<String>();

            using (SqlConnection connect = new SqlConnection(connectionString)
     )
            {
                connect.Open();

                using (SqlCommand command = new SqlCommand("select * from apbd.Subject", connect))
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        sqlresult.Add(reader[1].ToString());
                    }
            }

            return sqlresult;

        }

        public int addStudent(Student added)
        {
            int abstractId = 0;
            //student gotowy do dodania
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO apbd.Student(FirstName,LastName,Address,IndexNumber,IdStudies) " +
                    "values(@imie,@nazwisko,@adres,@nrindeksu,@idstudy); SELECT CAST(@@identity AS INT);", connection))
                {
                    command.Parameters.AddWithValue("@imie", added.imie);
                    command.Parameters.AddWithValue("@nazwisko", added.nazwisko);
                    command.Parameters.AddWithValue("@adres", added.adress);
                    command.Parameters.AddWithValue("@nrindeksu", added.nrindeksu);
                    command.Parameters.AddWithValue("@idstudy", Convert.ToInt32(added.idstudia));

                    int affectedRows = (int)command.ExecuteScalar();
                    Console.Write(affectedRows);
                    abstractId = affectedRows;
                }

                studentsList.Add(added);
                return abstractId;
            }
        }

        internal void editStudent(Student newSt)
        {
            foreach (Student t in studentsList)
                if (t.idStudent == newSt.idStudent)
                {
                    t.imie = newSt.imie;
                    t.nazwisko = newSt.nazwisko;
                    t.adress = newSt.adress;
                    t.nrindeksu = newSt.nrindeksu;
                    t.idstudia = newSt.idstudia;
                }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("update apbd.Student set FirstName=@imie,LastName=@nazwisko,Address=@adres,IndexNumber=@nrindeksu,IdStudies=@idstudy where IdStudent=@id", connection))
                {
                    command.Parameters.AddWithValue("@id", Convert.ToInt32(newSt.idStudent));
                    command.Parameters.AddWithValue("@imie", newSt.imie);
                    command.Parameters.AddWithValue("@nazwisko", newSt.nazwisko);
                    command.Parameters.AddWithValue("@adres", newSt.adress);
                    command.Parameters.AddWithValue("@nrindeksu", newSt.nrindeksu);
                    command.Parameters.AddWithValue("@idstudy", Convert.ToInt32(newSt.idstudia));
                    int affectedRows = command.ExecuteNonQuery();
                    Console.Write(affectedRows);
                }
            }
            */
        }
        
    }
}

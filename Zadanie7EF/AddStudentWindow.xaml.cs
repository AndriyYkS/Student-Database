using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Zadanie7EF.Models;

namespace Zadanie7EF
{
    /// <summary>
    /// Interaction logic for AddStudentWindow.xaml
    /// </summary>

    public delegate void AddStudentHandler(object sender, Student nStudent);

    public partial class AddStudentWindow : Window
    {
        public event AddStudentHandler AddStudent;

        private Student newStudent;
        public AddStudentWindow(List<Study> studies)
        {
            InitializeComponent();
            StudiesComboBox.ItemsSource = studies;
            //SubjectListBox.ItemsSource = przedmioty;

        }
        public AddStudentWindow(Student student, List<Study> studies)
        {
            InitializeComponent();
            newStudent = student;
            FillForm();
            StudiesComboBox.ItemsSource = studies;
          // SubjectListBox.ItemsSource

        }

        private void FillForm()
        {
            LastNameTxtBox.Text = newStudent.LastName;
            FirstNameTxtBox.Text = newStudent.FirstName;
            IndexTxtBox.Text = newStudent.IndexNumber;
            AdressTxtBox.Text = newStudent.Address;
            StudiesComboBox.SelectedItem = newStudent.Study.ToString();
            // StudiesComboBox.SelectedItem = Received.Study;
            // SubjectListBox.ItemsSource = Received.;

        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                MessageBox.Show("Incorrect data");
            }
            else
            {
                var nStudent = new Student
                {
                    FirstName = NormalizeInput(FirstNameTxtBox.Text),
                    LastName = NormalizeInput(LastNameTxtBox.Text),
                    IndexNumber = IndexTxtBox.Text,
                    Address = AdressTxtBox.Text,
                   // SubjectListBox = 
                    Study = StudiesComboBox.SelectedItem as Study

                };
                if (newStudent == null)
                {
                    AddStudent(this, nStudent);
                }
                    Close();
            }
        }
        private bool IsChanged(Student student)
        {
            return student.IndexNumber != newStudent.IndexNumber || student.FirstName != newStudent.FirstName
                || student.LastName != newStudent.LastName;
        }

        private bool ValidateInput()
        {
            var l_name = LastNameTxtBox.Text;
            var f_name = FirstNameTxtBox.Text;
            var index = IndexTxtBox.Text;
            if (string.IsNullOrWhiteSpace(l_name) || string.IsNullOrWhiteSpace(f_name) || string.IsNullOrWhiteSpace(index))
            {
                return false;
            }
            string index_pattern = "[s]\\d{5}";
            Regex regex = new Regex(index_pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var match = regex.Match(index);
            if (!match.Success)
            {
                return false;
            }

            return true;
        }
        private string NormalizeInput(string input)
        {
            input = Regex.Replace(input, @"\s+", "");
            return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
        }

        private void DelStudentButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SubjectListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StudiesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

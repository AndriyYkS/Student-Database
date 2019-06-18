using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zadanie7EF.Models;

namespace Zadanie7EF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DAL.EfServiceDb connection;

        List<Study> Studies;
        public MainWindow()
        {
            InitializeComponent();
            InitializeDBConn();
        }

        private void InitializeDBConn()
        {
            connection = new DAL.EfServiceDb();
            var obsColl = new ObservableCollection<Student>();
            foreach (Student student in connection.GetStudents()) {
                obsColl.Add(student);
            }
                DataGrid.ItemsSource = obsColl;
                Studies = new List<Study>();
                foreach (Study study in connection.GetStudies()) {
                Studies.Add(study);
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e) {

            var selected_itm = DataGrid.SelectedItems.Count;
            if (selected_itm > 0) {
                var selected = DataGrid.SelectedItems.Cast<Student>().ToList();
                var collection = DataGrid.ItemsSource as ObservableCollection<Student>;

                if (collection != null) {
                    if (MessageBox.Show($"Do you really want to delete ? {selected_itm} students?", "Question",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        foreach (Student student in selected) {
                            collection.Remove(student);
                            connection.RemoveStudentFromDB(student);
                        }
                    }
                }
            }
            connection.Commit();
        }

        private void AddNewStudentButton_Click(object sender, RoutedEventArgs e) {
            var addStudnetWindow = new AddStudentWindow(Studies);
            addStudnetWindow.AddStudent += new AddStudentHandler(AddStudentHandler);
            addStudnetWindow.ShowDialog();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            var data_g = sender as DataGrid;
            var selected_itm = data_g.SelectedItems.Count;
            if (selected_itm > 1) {
                HowManySelectedLabel.Content = "Wybrałeś " + selected_itm + " studentów";
            }
            else {
                HowManySelectedLabel.Content = "";
            }

        }
        private void AddStudentHandler(object sender, Student nStudent) {
            var source = DataGrid.ItemsSource as ObservableCollection<Student>;
            if (source != null) {
                source.Add(nStudent);
                connection.AddStudentToDB(nStudent);
            }

        }

        private void DataGrid_MouseDoubleClick(object obj, MouseButtonEventArgs e) {
            var _grid = obj as DataGrid;
            var selected = _grid.SelectedItem;
            if (selected != null) {
                var student = selected as Student;
                  connection.context.Students.Attach(student);
                var addWindow = new AddStudentWindow(student, Studies);
                 addWindow.ShowDialog();
            }
        }
    }
}

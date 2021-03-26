using DiaryWPF.Commands;
using DiaryWPF.Models;
using DiaryWPF.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DiaryWPF.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        public MainViewModel()
        {
            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);

            RefreshDiary();


            InitGroups();
        }

       
        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand RefreshStudentsCommand { get; set; }


        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set { _selectedGroupId = value; }
        }


        private ObservableCollection<Group> _group;

        public ObservableCollection<Group>Groups
        {
            get { return _group; }
            set { 
                _group = value;
                OnPropertyChange();

            }
        }


        private Student _selectedStudent;

        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set 
            { 
                _selectedStudent = value;
                OnPropertyChange();
            }
        }

        private ObservableCollection<Student> _students;

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChange();
            }
        }


        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }


       

        

        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null;
        }

        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Students Deletion", $"Do you really want to remove selected student: " +
                $"{SelectedStudent.FirstName} {SelectedStudent.LastName}", MessageDialogStyle.AffirmativeAndNegative);

            RefreshDiary();

        }

        private void AddEditStudent(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as Student);
            addEditStudentWindow.Closed += AddEditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }

        private void AddEditStudentWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary(); 
        }

        private void InitGroups()
        {
            Groups = new ObservableCollection<Group>
            {
                new Group{
                    Id=0,
                    Name="All"},
                new Group{
                    Id=1,
                    Name="2A"},
                new Group{
                    Id=2,
                    Name="2B"},
            };

            SelectedGroupId = 0;
        }

        private void RefreshDiary()
        {
            Students = new ObservableCollection<Student>
            {
                new Student { FirstName="Izabela",
                LastName="Winkler",
                Group = new Group {Id=1}
                }
                , new Student { FirstName="Horacy",
                LastName="Fikusny",
                Group = new Group {Id=2}
                }
                , new Student { FirstName="Kasia",
                LastName="Zdanowska",
                Group = new Group {Id=3}
                }
            };
        }
    }
}

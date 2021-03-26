using DiaryWPF.Commands;
using DiaryWPF.Models;
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
    public class AddEditStudentViewModel : ViewModelBase
    {


        public AddEditStudentViewModel(Student student = null)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);

            throw new Exception("BDBDB");

            if (student == null)
            {
                Student = new Student();
            }
            else
            {
                Student = student;
                IsUpdate = true;
            }

            InitGroups();
        }

        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }



        private Student _student;

        public Student Student 
        {
            get { return _student; }
            set { 
                _student = value;
                OnPropertyChange();
            }
        }

        private  bool _isUpdate;

        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                _isUpdate = value;
                OnPropertyChange();
            }
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set { _selectedGroupId = value; }
        }


        private ObservableCollection<Group> _group;

        public ObservableCollection<Group> Groups
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChange();

            }
        }
        
        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void Confirm(object obj)
        {
            if (!IsUpdate)
            {
                AddStudent();
            }
            else
            {
                UpdateStudent();
            }
            CloseWindow(obj as Window);
        }

        private void UpdateStudent()
        {
            //baza
        }

        private void AddStudent()
        {
           //baza
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
        private void InitGroups()
        {
            Groups = new ObservableCollection<Group>
            {
                new Group{
                    Id=0,
                    Name="-- none --"},
                new Group{
                    Id=1,
                    Name="2A"},
                new Group{
                    Id=2,
                    Name="2B"},
            };

            Student.Group.Id = 0;
        }

    }
}

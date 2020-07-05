using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;

namespace View_Model
{
    public class GamanetViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        //Employee that can be selected in listbox and deleted
        private Employee _EmployeeToDelete = new Employee();
        public Employee EmployeeToDelete
        {
            get { return _EmployeeToDelete; }
            set
            {
                _EmployeeToDelete = value;
                OnPropertyChanged("EmployeeToDelete");
            }
        }
        //List of added employees
        private ObservableCollection<Employee> _AllEmployees;
        public ObservableCollection<Employee> AllEmployees
        {
            get
            {
                if (_AllEmployees == null)
                {
                    _AllEmployees = new ObservableCollection<Employee>();
                }
                return _AllEmployees;
            }
            set
            {
                _AllEmployees = value;
                OnPropertyChanged("AllEmployees");
            }
        }
        //Variable for creating employee
        Employee _CurrentEmployee;
        public Employee CurrentEmployee
        {
            get
            {
                if (_CurrentEmployee == null)
                {
                    _CurrentEmployee = new Employee();
                }
                return _CurrentEmployee;
            }
            set
            {
                _CurrentEmployee = value;
                OnPropertyChanged("CurrentEmployee");
            }
        }
        //Command for adding employees
        private Command _AddEmployeesCommand;
        public Command AddEmployeesCommand
        {
            get
            {
                return _AddEmployeesCommand ??
                       (_AddEmployeesCommand = new Command(obj =>
                       {
                           CurrentEmployee.Age = DateTime.Now.Year - CurrentEmployee.Date.Year;
                           AllEmployees.Add(CurrentEmployee);
                           MessageBox.Show($"The work starts at: {DateTime.Now.ToShortTimeString()}\n" +
                $"Ends at: {DateTime.Now.AddHours(8).ToShortTimeString()}");
                           CurrentEmployee = new Employee();
                       }));
            }
        }
        //Command for deleting employees
        private Command _DeleteEmployeeCommand;
        public Command DeleteEmployeeCommand
        {
            get
            {
                return _DeleteEmployeeCommand ??
                       (_DeleteEmployeeCommand = new Command(obj =>
                       {
                           AllEmployees.Remove(EmployeeToDelete);
                       }));
            }
        }
        //Command for sorting list of employees
        //Command _SortEmployeesCommand;
        //public ICommand SortEmployeesCommand
        //{
        //    get
        //    {
        //        if (_SortEmployeesCommand == null)
        //            _SortEmployeesCommand = new Command(ExecuteSortEmployeesCommand, CanExecuteSortEmployeesCommand);
        //        return _SortEmployeesCommand;
        //    }
        //}
        //private void ExecuteSortEmployeesCommand(object parameter)
        //{
        //    var list = AllEmployees.ToList();
        //    list.Sort();
        //    AllEmployees.Clear();
        //    foreach(var i in list) AllEmployees.Add(i);
        //}
        //private bool CanExecuteSortEmployeesCommand(object parameter)
        //{
        //    return true;
        //}
    }
}

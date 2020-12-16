using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SalaryApp.Model.TableModels;
using System.Windows.Input;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System;
using SalaryApp.Model;
using SalaryApp.Model.FunctionalityProviders;
using System.Windows;

namespace SalaryApp.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        MainModel mainModel;
        public MainViewModel()
        {
            mainModel = new MainModel(SalaryProcesser.ShowSalaryInMsgBox,
                                      PasswordReceiver.GetPasswordFromPasswordWindow,
                                      PasswordCheck.CheckSha256Hash,
                                      SubordinatesViewer.ViewSubordinatesInWindow,
                                      PasswordReceiver.GetAdminPswInfo
                                      );
            Employees = mainModel.GetEmployeesDescription();
        }

        #region  private variables
        string _databaseFile;
        int _selectedEmployeeIdx;
        DateTime selectedDate;
        #endregion

        #region fields
        public ObservableCollection<CompanyEmployee> Employees { get; set; }

        public int SelectedEmployeeIdx
        {
            get => _selectedEmployeeIdx;
            set
            {
                _selectedEmployeeIdx = value;
                RaisePropertyChanged("SelectedEmployee");
            }
        }        
        public string DatabaseFile
        {
            get => _databaseFile;
            set
            {
                _databaseFile = value;
                RaisePropertyChanged("DatabaseFile");
            }
        }

        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                RaisePropertyChanged("SelectedDate");
            }
        }
        #endregion

        #region commands
        public ICommand CalculatePersonSalaryCommand
        {
            get => new RelayCommand(() =>
            {
                try
                {
                    mainModel.CalcAndShowSalary(SelectedEmployeeIdx, SelectedDate);                        
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            });
        }

        public ICommand CalculateOverallSalaryCommand
        {
            get => new RelayCommand(() =>
            {
                try
                {
                    mainModel.CalcAndShowCompanySalary(SelectedDate);
                }
                catch(Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            });
        }
        public ICommand RemoveSelectedEmployee
        {
            get => new RelayCommand(() =>
            {
                try
                {
                    mainModel.RemoveEmployee(
                        SelectedEmployeeIdx
                        );
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            });
        }
        public ICommand AddEmployeeCommand
        {
            get => new RelayCommand(() =>
            {
                try
                {
                    mainModel.AddEmployee();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            });
        }
        public ICommand ShowSubordinatesCommand
        {
            get => new RelayCommand(() =>
            {
                try
                {
                    mainModel.ShowSubordinates(SelectedEmployeeIdx);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            });
        }


        #endregion

    }
}
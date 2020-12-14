using ColleguesLibrary.Classes;
using SalaryApp.Model.TableModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SalaryApp.Model
{
    public class MainModel
    {
        public ClassConverter converter; //конвертер классов из БД в бизнес-модель
        public ApplicationContext context;
        public ObservableCollection<CompanyEmployee> EmployeesFromDb;
        
        public Action<double> SalaryProcessor;  //отображение заработной платы 
        public Func<string,string, bool> PasswordChecker; //алгоритм проверки пароля
        public Func<string> PasswordInput; //ввод пароля
        public Action<List<string>> SubordinatesViewer;
        public Func<string> AdminPasswordReceiver;

        public MainModel (Action<double> salaryProcessor,                           
                          Func<string> passwordInput,
                          Func<string, string, bool> passwordChecker,
                          Action<List<string>> subordinatesViewer,
                          Func<string> adminPasswordReceiver
            )
        {
            context = new ApplicationContext();
            context.CompanyEmployees.Load();
            EmployeesFromDb = context.CompanyEmployees.Local;
            converter = new ClassConverter(context);

            SalaryProcessor = salaryProcessor;
            PasswordInput= passwordInput;
            PasswordChecker = passwordChecker;
            SubordinatesViewer = subordinatesViewer;
            AdminPasswordReceiver = adminPasswordReceiver;
        }
        
        public ObservableCollection<CompanyEmployee> GetEmployeesDescription()
        {
            return EmployeesFromDb;
        }
        
        public double GetWage(int idx, DateTime wageTime)
        {
            Employee employee = converter.GetEmployee(EmployeesFromDb[idx]); 
            
            if(employee.EmploymentDate > wageTime)
            {
                throw new Exception("Выберите дату позднее даты трудоустройства сотрудника.");
            }
            string password = PasswordInput != null? PasswordInput():"";
            
            List<string> allPossiblePasswordInfos = new List<string>();// хеши сотрудника и его начальников
            
            allPossiblePasswordInfos.AddRange(converter.GetEmployeePasswords(EmployeesFromDb[idx]));
            string adminPswInfo = AdminPasswordReceiver();
            
            if (adminPswInfo != null)
            {
                allPossiblePasswordInfos.Add(adminPswInfo);
            }
            
            foreach(string passInfo in allPossiblePasswordInfos)
            {
                if (PasswordChecker!=null ? PasswordChecker(password, passInfo):true)
                {
                    return employee.GetWage(wageTime);
                }
            }
            throw new Exception("У Вас отсутствуют права доступа");      
        }

        public double GetCompanyWage(DateTime wageTime)
        {
            string password = PasswordInput != null ? PasswordInput() : "";
            if(password.Length > 0)
            {
                if (PasswordChecker(password, AdminPasswordReceiver()))
                {
                    double overallSalary = 0;
                    foreach (CompanyEmployee employee in context.CompanyEmployees.Local)
                    {
                        if (employee.EmploymentDate > wageTime)
                        {
                            continue;
                        }
                        Employee e = converter.GetEmployee(employee);
                        overallSalary += e.GetWage(wageTime);
                    }
                    return overallSalary;
                }
            }
            throw new Exception("У Вас отсутствуют права доступа");
        }
    
        public void RemoveEmployee(int idx)
        {
            context.CompanyEmployees.Remove(context.CompanyEmployees.Local[idx]);
            context.SaveChanges();
        }

        public void ShowSubordinates(int idx)
        {
            Employee employee = converter.GetEmployee(EmployeesFromDb[idx]);
            var subordinates = employee.GetAllSubordinates();
            List<string> subDescr;
            if (subordinates == null || subordinates.Count == 0)
            {
                subDescr = new List<string>();
                subDescr.Add("Нет подчинённых!");
            }
            else
            {
                subDescr = subordinates.Select(s => string.Format("{0}, трудоустоен {1}", s.Fio, s.EmploymentDate.ToString())).ToList();
            }
            SubordinatesViewer(subDescr);
        }

        public void AddEmployee()
        {
            MessageBox.Show("Не реализовано!");
        }
    }
}

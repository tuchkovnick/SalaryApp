using System;
using System.Collections.Generic;
using System.Text;
using ColleguesLibrary.Classes.Calculators;

namespace ColleguesLibrary.Classes
{
    public class EmployeeSalaryCalculator
    {
        private static EmployeeSalaryCalculator _instance = null;
        public static EmployeeSalaryCalculator GetInstance()
        {
            return _instance ?? (_instance = new EmployeeSalaryCalculator());
        }

        private ICalculator _calculator;

        public double CalculateSalary(Employee employee, DateTime time)
        {
            switch (employee.Type)
            {
                case EmployeeType.Worker:
                {
                    _calculator = new WorkerCalculator();
                   break;
                }
                case EmployeeType.Manager:
                {
                    _calculator = new ManagerCalculator();
                    break;
                }
                case EmployeeType.Salesman:
                {
                    _calculator = new WorkerCalculator();
                    break;
                }
                default:
                    throw new Exception("Employee type undefined");
            }

            return _calculator.CalcSalary(employee, time);
        }


    }
}

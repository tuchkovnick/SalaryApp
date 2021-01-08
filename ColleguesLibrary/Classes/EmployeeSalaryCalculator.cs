using System;
using System.Collections.Generic;
using System.Text;

namespace ColleguesLibrary.Classes
{
    public class EmployeeSalaryCalculator
    {
        private static EmployeeSalaryCalculator _instance = null;
        public static EmployeeSalaryCalculator GetInstance()
        {
            return _instance ?? (_instance = new EmployeeSalaryCalculator());
        }

        private Func<Employee, DateTime, double> _salaryCalcFunction;

        public double CalculateSalary(Employee employee, DateTime time)
        {
            switch (employee.Type)
            {
                case EmployeeType.Worker:
                {
                    _salaryCalcFunction = CalcFunctions.CalcWorkerSalary;
                   break;
                }
                case EmployeeType.Manager:
                {
                    _salaryCalcFunction = CalcFunctions.CalcManagerSalary;
                    break;
                }
                case EmployeeType.Salesman:
                {
                    _salaryCalcFunction = CalcFunctions.CalcSalesmanSalary;
                    break;
                }
            }

            return _salaryCalcFunction(employee, time);
        }


    }
}

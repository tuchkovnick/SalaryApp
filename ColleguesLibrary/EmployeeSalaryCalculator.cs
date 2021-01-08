using System;
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

        public double CalculateSalary(Employee employee, DateTime time)
        {
            return CalculatorFabric.GetCalculator(employee.Type).CalcSalary(employee, time);
        }


    }
}

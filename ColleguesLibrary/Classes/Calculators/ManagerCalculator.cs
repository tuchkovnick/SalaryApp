using System;
using System.Collections.Generic;
using System.Text;

namespace ColleguesLibrary.Classes.Calculators
{
    internal class ManagerCalculator : ICalculator
    {
        public double CalcSalary(Employee employee, DateTime time)
        {
            var wage = employee.WageRate;
            var span = time - employee.EmploymentDate;
            int years = span.Days / 365;

            if (years > 0)
            {
                var maxWage = wage * 1.4;
                while (years-- > 0 && wage < maxWage)
                {
                    wage += employee.WageRate * 0.05;
                }
            }

            if (employee.GetDirectSubordinates() != null)
            {
                double subordinateWage = 0;
                foreach (Employee empl in employee.GetDirectSubordinates())
                {
                    subordinateWage += EmployeeSalaryCalculator.GetInstance().CalculateSalary(empl, time);
                }
                wage += subordinateWage * 0.05;
            }

            return Math.Round(wage, 2);
        }
    }
}

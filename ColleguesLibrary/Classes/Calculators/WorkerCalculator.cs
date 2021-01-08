using System;
using System.Collections.Generic;
using System.Text;

namespace ColleguesLibrary.Classes.Calculators
{
    internal class WorkerCalculator : ICalculator
    {
        public double CalcSalary(Employee employee, DateTime time)
        {
            var wage = employee.WageRate;
            var span = time - employee.EmploymentDate;
            int years = span.Days / 365;
            var maxWage = employee.WageRate * 1.3;
            while (years-- > 0 && wage < maxWage)
            {
                wage += employee.WageRate * 0.03;
            }

            return Math.Round(wage, 2);
        }
    }
}

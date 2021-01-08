using System;
using System.Collections.Generic;
using System.Text;

namespace ColleguesLibrary.Classes
{
    public static class CalcFunctions
    {
        public static double CalcWorkerSalary(Employee employee, DateTime time)
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

        public static double CalcManagerSalary(Employee employee, DateTime time)
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
                    subordinateWage += EmployeeSalaryCalculator.GetInstance().CalculateSalary(empl,time);
                }
                wage += subordinateWage * 0.05;
            }

            return Math.Round(wage, 2);
        }

        public static double CalcSalesmanSalary(Employee employee, DateTime time)
        {
            var wage = employee.WageRate;
            var span = time - employee.EmploymentDate;
            int years = span.Days / 365;

            if (years > 0)
            {
                var maxWage = wage * 1.35;
                while (years-- > 0 && wage < maxWage)
                {
                    wage += employee.WageRate * 0.01;
                }
            }
            
            double subordinateWage = 0;
            foreach (Employee empl in employee.GetAllSubordinates())
            {
                subordinateWage += EmployeeSalaryCalculator.GetInstance().CalculateSalary(empl, time);
            }
            wage += subordinateWage * 0.003;

            return Math.Round(wage, 2);
        }
    }
}

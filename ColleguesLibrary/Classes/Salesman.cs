using ColleguesLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColleguesLibrary.Classes
{
    public class Salesman : Manager
    {
        public Salesman(int Id, 
                        string Fio, 
                        double WageRate, 
                        DateTime EmploumentDate, 
                        List<Employee> Subordinates)
           : base(Id, Fio, WageRate, EmploumentDate, Subordinates) { }

        public override double GetWage(DateTime time)
        {
            var Wage = WageRate;
            var span = time - EmploymentDate;
            int years = span.Days / 365;

            if (years > 0)
            {
                var MaxWage = Wage * 1.35;
                while (years-- > 0 && Wage < MaxWage)
                {
                    Wage += WageRate * 0.01;
                }
            }

            if (DirectSubordinates.Count > 0)
            {
                double subordinateWage = 0;
                foreach (Employee employee in GetAllSubordinates())
                {
                    subordinateWage += employee.GetWage(time);
                }
                Wage += subordinateWage * 0.003;
            }

            return Math.Round(Wage, 2);
        }
    }
}

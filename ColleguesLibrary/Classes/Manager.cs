using ColleguesLibrary.Classes;
using ColleguesLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColleguesLibrary.Classes
{
    public class Manager : Employee, IBoss
    {        
        //прямые подчиненнык
        public List<Employee> DirectSubordinates;
        public Manager(int Id, 
                       string Fio, 
                       double WageRate, 
                       DateTime EmploumentDate, 
                       List<Employee> DirectSubordinates
            )
            :base(Id, Fio, WageRate, EmploumentDate)
        {
            this.DirectSubordinates = DirectSubordinates;
        }
        public override List<Employee> GetAllSubordinates() 
        {
            List<Employee> subordinates = new List<Employee>(DirectSubordinates);
            foreach(Employee e in DirectSubordinates)
            {
                if(e is IBoss)
                {
                    subordinates.AddRange((e as IBoss).GetAllSubordinates());
                }
            }
            return subordinates;
        }
        public override double GetWage(DateTime time)
        {
            var Wage = WageRate;
            var span = time - EmploymentDate;
            int years = span.Days / 365;

            if (years > 0)
            {
                var MaxWage = Wage * 1.4;
                while (years-- > 0 && Wage < MaxWage)
                {
                    Wage += WageRate * 0.05;
                }
            }
            
            if(DirectSubordinates.Count > 0)
            {
                double subordinateWage = 0;
                foreach (Employee employee in DirectSubordinates)
                {
                    subordinateWage += employee.GetWage(time);
                }
                Wage += subordinateWage * 0.05;
            }

            return Math.Round(Wage, 2);
        }

    }
}

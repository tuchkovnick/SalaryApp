using ColleguesLibrary.Interfaces;
using System;
using System.Collections.Generic;

namespace ColleguesLibrary.Classes
{
    public class Employee : IWorker
    {
        #region private variables
        private int _id;
        private string _fio;
        private double _wageRate;
        private DateTime _employmentDate;
        #endregion
        public int Id
        {
            get => _id;
            set => _id = value;
        }       
        public string Fio
        {
            get => _fio;
            set => _fio = value;
        }        
        public double WageRate
        {
            get => _wageRate;
            set => _wageRate = value;
        }
        public DateTime EmploymentDate
        {
            get => _employmentDate;
            set => _employmentDate = value;
        }
        public Employee(int Id, 
                        string Fio, 
                        double WageRate, 
                        DateTime EmploymentDate
                        )
        {
            this.Id = Id;
            this.Fio = Fio;
            this.WageRate = WageRate;
            this.EmploymentDate = EmploymentDate;
        }
        public virtual double GetWage(DateTime time)
        {
            var Wage = WageRate;            
            var span = time - EmploymentDate;
            int years = span.Days / 365;
            var MaxWage = WageRate * 1.3;
            while (years-- > 0 && Wage < MaxWage)
            {
                Wage += WageRate * 0.03;
            }

            return Math.Round(Wage,2);
        }    
       
        public virtual List<Employee> GetAllSubordinates()
        {
            return null;
        }
    }
}

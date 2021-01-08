using System;
using System.Collections.Generic;

namespace ColleguesLibrary.Classes
{
    public enum EmployeeType
    {
        Worker,
        Manager,
        Salesman
    }
    public class Employee
    {
        public string Fio { get; set; }
        public double WageRate { get; set; }
        public DateTime EmploymentDate { get; set; }
        public EmployeeType Type { get; set; }
        public Employee(string fio,  double wageRate, DateTime employmentDate)
        {
            Fio = fio;
            WageRate = wageRate;
            EmploymentDate = employmentDate;
            Type = type;
        }

        private readonly Lazy<List<Employee>> _subordinates =
            new Lazy<List<Employee>>();
        
        public Employee SetType(EmployeeType type)
        {
            Type = type;
            return this;
        }
        
        public void AddSubordinate(Employee subordinate)
        {
            if (Type != EmployeeType.Worker)
            {
                _subordinates.Value.Add(subordinate);
            }
        }
        
        public List<Employee> GetDirectSubordinates()
        {
            return Type == EmployeeType.Worker? null : _subordinates.Value;
        }
        
        public List<Employee> GetAllSubordinates()
        {
            if(Type == EmployeeType.Worker)
                return null;
            
            List<Employee> allSubordinates = new List<Employee>(_subordinates.Value);
            foreach (var employee in _subordinates.Value)
            {
                allSubordinates.AddRange(employee.GetAllSubordinates());
            }

            return allSubordinates;
        }
    }
}

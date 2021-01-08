using System;
using System.Collections.Generic;
using System.Text;

namespace ColleguesLibrary.Classes.Calculators
{
    public interface ICalculator
    {
        double CalcSalary(Employee employee, DateTime time);
    }
}

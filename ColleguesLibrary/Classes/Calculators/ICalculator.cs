using System;
using System.Collections.Generic;
using System.Text;

namespace ColleguesLibrary.Classes.Calculators
{
    internal interface ICalculator
    {
        double CalcSalary(Employee employee, DateTime time);
    }
}

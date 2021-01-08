using System;
using System.Collections.Generic;
using System.Text;

namespace ColleguesLibrary.Classes.Calculators
{
    internal static class CalculatorFabric
    {
        public static ICalculator GetCalculator(EmployeeType type)
        {
            switch (type)
            {
                case EmployeeType.Worker:
                    return new WorkerCalculator();
                case EmployeeType.Manager:
                        return new ManagerCalculator();
                case EmployeeType.Salesman:
                    return new WorkerCalculator();
                default:
                    throw new Exception("Undefined employee type!");
            }
        }
    }
}

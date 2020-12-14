using ColleguesLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColleguesLibrary.Interfaces
{
    public interface IBoss
    {
        List<Employee> GetAllSubordinates();
    }
}

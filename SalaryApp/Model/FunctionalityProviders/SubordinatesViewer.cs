using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryApp.Model.FunctionalityProviders
{
    //отображение подчинённых
    public class SubordinatesViewer
    {
        public static void ViewSubordinatesInWindow(List<string> subordinates)
        {
            SubordianatesWindow sw = new SubordianatesWindow(subordinates);
            sw.ShowDialog();
        }
    }
}

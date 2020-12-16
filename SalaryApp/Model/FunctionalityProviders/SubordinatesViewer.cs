using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SalaryApp.Model.FunctionalityProviders
{
    //отображение подчинённых
    public class SubordinatesViewer
    {
        public static void ViewSubordinatesInWindow(List<string> subordinates)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                SubordianatesWindow sw = new SubordianatesWindow(subordinates);
                sw.ShowDialog();
            });

        }
    }
}

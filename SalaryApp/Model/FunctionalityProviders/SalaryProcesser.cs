using System.Windows;

namespace SalaryApp.Model.FunctionalityProviders
{
    static class SalaryProcesser
    {
        //отображение пароля
        public static void ShowSalaryInMsgBox (double salary)
        {
            MessageBox.Show(string.Format("{0} руб.", salary.ToString()));
        }
    }
}

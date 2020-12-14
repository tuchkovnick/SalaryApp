using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryApp.Model.FunctionalityProviders
{
    public static class PasswordReceiver
    {
        //интерфейс ввода пароля
        public static string GetPasswordFromPasswordWindow()
        {
            PasswordCheckWindow pcw = new PasswordCheckWindow();
            pcw.ShowDialog();
            return pcw.password;
        }

        //получение хеша пароля администратора
        public static string GetAdminPswInfo()
        {
            return File.ReadAllText("AdminPswInfo.txt");
        }
    }
}

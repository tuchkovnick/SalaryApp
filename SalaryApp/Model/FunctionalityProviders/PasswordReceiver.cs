using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SalaryApp.Model.FunctionalityProviders
{
    public static class PasswordReceiver
    {
        //интерфейс ввода пароля
        public static string GetPasswordFromPasswordWindow()
        {
            string password = Application.Current.Dispatcher.Invoke(() =>
            {
                PasswordCheckWindow pcw = new PasswordCheckWindow();
                pcw.ShowDialog();
                return pcw.password;
            });
            return password;
        }

        //получение хеша пароля администратора
        public static string GetAdminPswInfo()
        {
            //хеш слова Admin
            return "c1c224b03cd9bc7b6a86d77f5dace40191766c485cd55dc48caf9ac873335d6f";
        }
    }
}

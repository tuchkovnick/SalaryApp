using SalaryApp.Model.TableModels;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SalaryApp.Model.FunctionalityProviders
{
    public class PasswordCheck
    {
        //функция проверки пароля
        public static bool CheckSha256Hash(string password, string passwordInfo)
        {
            using (SHA256 hash = SHA256.Create())
            {
                return String.Concat(hash
                  .ComputeHash(Encoding.UTF8.GetBytes(password))
                  .Select(item => item.ToString("x2"))) == passwordInfo;
            }
        }
    }
}

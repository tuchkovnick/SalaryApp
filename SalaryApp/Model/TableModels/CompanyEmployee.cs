using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SalaryApp.Model.TableModels
{
    public class CompanyEmployee : INotifyPropertyChanged
        {
            #region private variables
            
            private string fio;
            private DateTime employmentDate;
            private string position;
            private double wagebase;
            private string passinfo;

        #endregion

            [Browsable(false)]
            public int Id { get;set; }
            [DisplayName("ФИО")]
            public string FIO
            {
                get { return fio; }
                set
                {
                    fio = value;
                    OnPropertyChanged("FIO");
                }
            }
            [DisplayName("Дата трудоустройства")]
            public DateTime EmploymentDate
            {
                get { return employmentDate; }
                set
                {
                    employmentDate = value;
                    OnPropertyChanged("EmploymentDate");
                }
            }
            
            [DisplayName("Должность")]
            public string Position
            {
                get { return position; }
                set
                {
                    position = value;
                    OnPropertyChanged("Position");
                }
            }
            [DisplayName("Ставка з/п")]
            public double WageBase
            {
                get { return wagebase; }
                set
                {
                    wagebase = value;
                    OnPropertyChanged("WageBase");
                }
            }
            
            [Browsable(false)]
            public string PassInfo
            {
                get { return passinfo; }
                set
                {
                    passinfo = value;
                    OnPropertyChanged("PassInfo");
                }
            }
            
            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged([CallerMemberName]string prop = "")
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }

            public override string ToString()
            {
                return string.Format("{0} (трудоустроен {1})", FIO, EmploymentDate.ToString());                
            }
    }
}

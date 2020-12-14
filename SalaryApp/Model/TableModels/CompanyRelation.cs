using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SalaryApp.Model.TableModels
{
    public class CompanyRelation : INotifyPropertyChanged
    {
        #region private variables
        int bossId;
        int subordinateId;
        #endregion
        public int Id { set; get; }
        public int BossId
        {
            get { return bossId; }
            set
            {
                bossId = value;
                OnPropertyChanged("BossId");
            }
        }
        public int SubordinateId
        {
            get { return subordinateId; }
            set
            {
                subordinateId = value;
                OnPropertyChanged("subordinateId");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

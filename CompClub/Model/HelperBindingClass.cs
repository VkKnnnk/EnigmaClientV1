using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CompClub.Model
{
    public class HelperBindingClass : INotifyPropertyChanged
    {
        private Sessions _session;
        public Users _user { get; set; }
        public Sessions session
        {
            get
            { return _session; }
            set
            {
                _session = value;
                OnPropertyChanged("session");
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

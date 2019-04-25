using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseProfiles.ViewModels
{
    class MouseProfileViewModel : INotifyPropertyChanged
    {
        private uint mouseSpeed;
        public uint MouseSpeed
        {
            get
            {
                // TODO service
                return mouseSpeed;
            }

            set
            {
                // TODO service
                NotifyPropertyChanged("MouseSpeed");
            }
        }
        public uint WheelLines { get; set; }
        public uint DoubleClickTime { get; set; }
        public bool MouseButtonSwap { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

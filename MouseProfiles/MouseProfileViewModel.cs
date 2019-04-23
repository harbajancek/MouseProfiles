using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseProfiles
{
    class MouseProfileViewModel : INotifyPropertyChanged
    {

        public MouseProfileViewModel(MouseProfile mouseProfile)
        {
            MouseProfile = mouseProfile;
        }
        public MouseProfile MouseProfile { get; set; }

        public UInt32 MouseSpeed { get { return MouseProfile.MouseSpeed; } set { MouseProfile.MouseSpeed = value; NotifyPropertyChanged("MouseSpeed"); } }
        public UInt32 WheelLines { get { return MouseProfile.WheelLines; } set { MouseProfile.WheelLines = value; NotifyPropertyChanged("WheelLines"); } }
        public UInt32 DoubleClickTime { get { return MouseProfile.DoubleClickTime; } set { MouseProfile.DoubleClickTime = value; NotifyPropertyChanged("DoubleClickTime"); } }
        public bool MouseButtonSwap { get { return MouseProfile.MouseButtonSwap; } set { MouseProfile.MouseButtonSwap = value; NotifyPropertyChanged("MouseButtonSwap"); } }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

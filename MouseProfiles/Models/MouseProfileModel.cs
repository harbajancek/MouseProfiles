using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseProfiles.Models 
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MouseProfileModel : INotifyPropertyChanged
    {
        private string name;
        [JsonProperty]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }
        private uint mouseSpeed;
        [JsonProperty]
        public uint MouseSpeed
        {
            get { return mouseSpeed; }
            set
            {
                mouseSpeed = value;
                RaisePropertyChanged("MouseSpeed");
            }
        }
        private uint wheelLines;
        [JsonProperty]
        public uint WheelLines
        {
            get { return wheelLines; }
            set
            {
                wheelLines = value;
                RaisePropertyChanged("MouseSpeed");
            }
        }
        private uint doubleClickTime;
        [JsonProperty]
        public uint DoubleClickTime
        {
            get { return doubleClickTime; }
            set
            {
                doubleClickTime = value;
                RaisePropertyChanged("DoubleClickTime");
            }
        }
        private bool mouseButtonSwap;
        [JsonProperty]
        public bool MouseButtonSwap
        {
            get { return mouseButtonSwap; }
            set
            {
                mouseButtonSwap = value;
                RaisePropertyChanged("MouseButtonSwap");
            }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

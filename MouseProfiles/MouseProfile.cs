using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseProfiles
{
    [JsonObject(MemberSerialization.OptIn)]
    class MouseProfile
    {
        private static string filterString;
        public static ObservableCollection<MouseProfile> MouseProfiles = new ObservableCollection<MouseProfile>();
        public static void Activator(MouseProfile profile)
        {
            foreach (var item in MouseProfiles)
            {
                item.Activated = false;
            }

            profile.Activate();
        }
        public string Name { get; set; }
        public bool Activated { get; private set; }
        const int SPI_SETMOUSESPEED = 0x0071;
        const int SPI_SETDOUBLECLICKTIME = 0x0020;
        const int SPI_SETMOUSEBUTTONSWAP = 0x0021;
        const int SPI_GETWHEELSCROLLLINES = 0x0068;
        private uint _mouseSpeed;
        private uint _wheelLines;
        private uint _doubleClickTime;
        private bool _mouseButtonSwap;

        [DllImport("User32.dll")]
        static extern Boolean SystemParametersInfo(
            UInt32 uiAction,
            object uiParam,
            UInt32 pvParam,
            UInt32 fWinIni);

        [JsonProperty]
        public UInt32 MouseSpeed
        {
            get { return _mouseSpeed; }
            set
            {
                _mouseSpeed = value;
                UpdateParameters();
            }
        }
        [JsonProperty]
        public UInt32 WheelLines
        {
            get { return _wheelLines; }
            set
            {
                _wheelLines = value;
                UpdateParameters();
            }
        }
        [JsonProperty]
        public UInt32 DoubleClickTime
        {
            get { return _doubleClickTime; }
            set
            {
                _doubleClickTime = value;
                UpdateParameters();
            }
        }
        [JsonProperty]
        public bool MouseButtonSwap
        {
            get { return _mouseButtonSwap; }
            set
            {
                _mouseButtonSwap = value;
                UpdateParameters();
            }
        }
        public void Activate()
        {
            Activated = true;
            UpdateParameters();
        }

        public void UpdateParameters()
        {
            SystemParametersInfo(
                SPI_SETMOUSESPEED,
                MouseSpeed,
                0,
                0);
            SystemParametersInfo(
                SPI_GETWHEELSCROLLLINES,
                WheelLines,
                0,
                0);
            SystemParametersInfo(
                SPI_SETDOUBLECLICKTIME,
                DoubleClickTime,
                0,
                0);
            SystemParametersInfo(
                SPI_SETMOUSEBUTTONSWAP,
                MouseButtonSwap,
                0,
                0);
        }
    }
}

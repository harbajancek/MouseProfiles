using MouseProfiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseProfiles.Services
{
    public class MouseProfileService
    {
        public MouseProfileService()
        {
            Database = new Database("Profiles");
        }
        private Database Database { get; set; }
        public  IEnumerable<MouseProfileModel> GetProfiles()
        {
            return Database.GetMouseProfiles();
        }
        public void SaveProfiles(IEnumerable<MouseProfileModel> profiles)
        {
            List<MouseProfileModel> profilees = profiles.Except(new List<MouseProfileModel>() { profiles.First() }).ToList();
            Database.SaveMouseProfiles(profilees);
        }
        const int SM_SWAPBUTTON = 0x0017;
        const int SPI_GETMOUSESPEED = 0x0070;
        const int SPI_SETMOUSESPEED = 0x0071;
        const int SPI_SETDOUBLECLICKTIME = 0x0020;
        const int SPI_SETMOUSEBUTTONSWAP = 0x0021;
        const int SPI_GETWHEELSCROLLLINES = 0x0068;
        const int SPI_SETWHEELSCROLLLINES = 0x0069;
        
        class SPI_SetMouseInt
        {
            [DllImport("User32.dll")]
            internal static extern bool SystemParametersInfo(
            uint uiAction,
            uint uiParam,
            uint pvParam,
            uint fWinIni);
        }
        class SPI_SetMouseBool
        {
            [DllImport("User32.dll")]
            internal static extern bool SystemParametersInfo(
            uint uiAction,
            bool uiParam,
            uint pvParam,
            uint fWinIni);
        }
        class SPI_GetMouseInt
        {
            [DllImport("User32.dll")]
            internal static extern bool SystemParametersInfo(
            uint uiAction,
            uint uiParam,
            IntPtr pvParam,
            uint fWinIni);
        }
        [DllImport("User32.dll")]
        static extern uint GetDoubleClickTime();
        [DllImport("User32.dll")]
        static extern int GetSystemMetrics(
            int nIndex);
        public void ApplyProfile(MouseProfileModel profile)
        {
            SPI_SetMouseInt.SystemParametersInfo(
                   SPI_SETMOUSESPEED,
                   0,
                   profile.MouseSpeed,
                   0);
            SPI_SetMouseInt.SystemParametersInfo(
                SPI_SETDOUBLECLICKTIME,
                profile.DoubleClickTime,
                0,
                0);
            SPI_SetMouseInt.SystemParametersInfo(
                SPI_SETWHEELSCROLLLINES,
                profile.WheelLines,
                0,
                0);
            SPI_SetMouseBool.SystemParametersInfo(
                SPI_SETMOUSEBUTTONSWAP,
                profile.MouseButtonSwap,
                0,
                0);
        }
        public MouseProfileModel GetDefaultMouseProfile()
        {
            MouseProfileModel profile = new MouseProfileModel()
            {
                MouseSpeed = 10,
                WheelLines = 3,
                DoubleClickTime = 500,
                MouseButtonSwap = false,
                Name = "Default"
            };
            return profile;
        }
        public unsafe MouseProfileModel GetCurrentMouseProfile()
        {
            MouseProfileModel profile = new MouseProfileModel();
            int utemp;
            SPI_GetMouseInt.SystemParametersInfo(
                SPI_GETMOUSESPEED,
                0,
                new IntPtr(&utemp),
                0);
            profile.MouseSpeed = (uint)utemp;

            SPI_GetMouseInt.SystemParametersInfo(
                SPI_GETWHEELSCROLLLINES,
                0,
                new IntPtr(&utemp),
                0);
            profile.WheelLines = (uint)utemp;
            profile.DoubleClickTime = GetDoubleClickTime();
            int temp = GetSystemMetrics(SM_SWAPBUTTON);
            profile.MouseButtonSwap = (temp != 0) ? true : false;
            profile.Name = "Current";
            return profile;
        }
    }
}

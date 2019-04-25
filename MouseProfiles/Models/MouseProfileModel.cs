using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseProfiles.Models
{
    class MouseProfileModel
    {
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public uint MouseSpeed { get; set; }
        [JsonProperty]
        public uint WheelLines { get; set; }
        [JsonProperty]
        public uint DoubleClickTime { get; set; }
        [JsonProperty]
        public bool MouseButtonSwap { get; set; }
    }
}

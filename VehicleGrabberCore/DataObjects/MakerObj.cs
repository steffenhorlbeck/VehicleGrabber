using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleGrabberCore.DataObjects
{
    public class MakerObj
    {
        public MakerObj()
        {
            MakerName = string.Empty;
            MakerUrlPath = string.Empty;
            MakerLogoBase64 = string.Empty;
            MakerLogoUrl = string.Empty;
            MakerLogoLocalFile = string.Empty;
        }

        public string MakerName { get; set; }
        public string MakerUrlPath { get; set; }
        public string MakerLogoBase64 { get; set; }
        public string MakerLogoUrl { get; set; }
        public string MakerLogoLocalFile { get; set; }
    }
}

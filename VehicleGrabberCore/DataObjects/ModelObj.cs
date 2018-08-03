using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleGrabberCore.DataObjects
{
    public class ModelObj
    {
        public ModelObj()
        {
            ModelID = -1;
            ModelName = string.Empty;
            ModelUrlPath = string.Empty;
            ModelImgBase64 = string.Empty;
            ModelThumbUrl = string.Empty;
            ModelLocalFile = string.Empty;
            MakerName = string.Empty;
            MakerUrlPath = string.Empty;
            MakerLogoBase64 = string.Empty;
            MakerLogoUrl = string.Empty;
        }
        
        public long ModelID { get; set; }
        public string ModelName { get; set; }
        public string ModelUrlPath { get; set; }
        public string ModelImgBase64 { get; set; }
        public string ModelThumbUrl { get; set; }
        public string ModelLocalFile { get; set; }
        // the infos from MakerObj
        public string MakerName { get; set; }
        public string MakerUrlPath { get; set; }
        public string MakerLogoBase64 { get; set; }
        public string MakerLogoUrl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleGrabberCore.DataObjects
{
    public class ModelTypeObj
    {
        public ModelTypeObj()
        {
            ModelID = -1;
            ModelTypeID = -1;
            MakerName = string.Empty;
            ModelName = string.Empty;
            ModelTypeName = string.Empty;
            ModelTypeCubic = string.Empty;
            ModelTypeFuel = string.Empty;
            ModelTypePower = string.Empty;
            ModelTypeTank = string.Empty;
            ModelTypeFromYear = string.Empty;
            ModelTypeToYear = string.Empty;
            ModelTypeDetailsUrl = string.Empty;
            ModelTypeChassis = string.Empty;
            ModelTypeDoors = 0;
        }

        public long ModelID { get; set; }
        public long ModelTypeID { get; set; }
        public string MakerName { get; set; }
        public string ModelName { get; set; }
        public string ModelTypeName { get; set; }
        public string ModelTypeCubic { get; set; }
        public string ModelTypeFuel { get; set; }
        public string ModelTypePower { get; set; }
        public string ModelTypeTank { get; set; }
        public string ModelTypeFromYear { get; set; }
        public string ModelTypeToYear { get; set; }
        public string ModelTypeDetailsUrl { get; set; }
        public string ModelTypeChassis { get; set; }
        public int ModelTypeDoors { get; set; }
    }
}

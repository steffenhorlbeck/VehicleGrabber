using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleGrabberCore.Helper
{
    public static class IntExtension
    {
        public static int ToInt32OrDefault(this string value, int defaultValue = 0)
        {
            return int.TryParse(value, out var result) ? result : defaultValue;
        }
    }
}

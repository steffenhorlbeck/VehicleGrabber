using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleGrabberCore.Configuration
{
    public class ConstantDefs
    {
        private static List<string> delimiterList = new List<string>() { ",", ";", "|" };

        /// <summary>List of available date separators</summary>
        private static List<string> dateSeparatorList = new List<string>() { "/", ".", "-" };

        /// <summary>List of available delimiters</summary>
        private static List<string> decimalSeparatorList = new List<string>() { ".", ";" };

        /// <summary>Gets or sets property to access delimiter list</summary>
        public static List<string> DelimiterList { get => delimiterList; set => delimiterList = value; }

        /// <summary>Gets or sets property to access date separator list</summary>
        public static List<string> DateSeparatorList { get => dateSeparatorList; set => dateSeparatorList = value; }

        /// <summary>Gets or sets property to access decimal separator list</summary>
        public static List<string> DecimalSeparatorList { get => decimalSeparatorList; set => decimalSeparatorList = value; }
    }
}

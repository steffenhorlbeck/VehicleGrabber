// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EncodingHelper.cs" company="NP4 GmbH">
//   All rights reserved
// </copyright>
// <summary>
//   Class to provide import functionality
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace VehicleGrabberCore.Helper
{
    /// <summary>
    /// Hilfsklasse für das Encoding
    /// </summary>
    public class EncodingHelper
    {
        #region Detect Encoding

        /// <summary>
        /// Gibt das Encoding eine Datei zurück.
        /// </summary>
        /// <param name="file">Die Datei, die geprüft werden soll.</param>
        /// <returns>Das entsprechend gefundene Encoding. Wenn keins gefunden wurde, wird Encoding.Default zurückgegeben.</returns>
        public static Encoding DetectEncoding(string file, string defaultEncoding = "")
        {
            FileInfo finfo = new FileInfo(file);
            FileStream fs = null;

            // try to get the BOM of the file and set the encoding based on it.
            // if no BOM was found, set the systems Encode.Default
            bool bomExists = false;
            Encoding res = GetEncodingByBom(file, out bomExists);

            // if a special encoding string was submitted, we try to use this one as default
            if (!string.IsNullOrEmpty(defaultEncoding))
            {
                res = GetCurrentEncoding(defaultEncoding);

            }

            // if no BOM exists read the files chars and try to get the encoding by reading first up to 100 lines
            if (!bomExists)
            {
                try
                {
                    fs = finfo.OpenRead();
                    res = DetectEncoding(fs, res);
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
                finally
                {
                    if (fs != null) fs.Close();
                }
            }

            return res;
        }


        /// <summary>
        /// Determines a text file's encoding by analyzing its byte order mark (BOM).
        /// Defaults to ASCII when detection of the text file's endianness fails.
        /// </summary>
        /// <param name="filename">The text file to analyze.</param>
        /// <returns>The detected encoding.</returns>
        public static Encoding GetEncodingByBom(string filename, out bool bomFound)
        {
            bomFound = true;
            // Read the BOM
            var bom = new byte[5];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 5);
            }

            // Analyze the BOM old
            /*
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
            */

            // analyze BOM new
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.Unicode;
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
            if (bom[0] == 0xFE && bom[1] == 0xFF) return Encoding.GetEncoding(1201); // 1201 unicodeFFFE Unicode (Big-Endian)
            if (bom[0] == 0xFF && bom[1] == 0xFE) return Encoding.GetEncoding(1200); // 1200 utf-16 Unicode



            bomFound = false;
            return Encoding.Default;
        }


        /// <summary>Get the current encoding information</summary>
        /// <param name="encoding">string - Encoding information (eg. UTF-8)</param>
        /// <returns>Encoding object for the current encoding information</returns>
        public static Encoding GetCurrentEncoding(string encoding)
        {
            Encoding encode = null;
            try
            {
                if (encoding != null)
                {
                    encode = Encoding.GetEncoding(encoding);
                }
            }
            catch (Exception e)
            {
                //If get encoding by string fails we need to check the string.
                // For ANSI there is no dedicated codepage for example so we need to catch such things here
                // in most cases codepage 1252 is used for ANSI 
                if (encoding.Equals("ANSI"))
                {
                    encode = Encoding.GetEncoding(1252);
                }
            }

            return encode;
        }

        /// <summary>
        /// Gibt das Encoding eine Datei zurück.
        /// </summary>
        /// <param name="stream">Der Stream der geprüft werden soll.</param>
        /// <returns>Das entsprechend gefundene Encoding. Wenn keins gefunden wurde, wird Encoding.Default zurückgegeben.</returns>
        public static Encoding DetectEncoding(FileStream stream)
        {
            return DetectEncoding(stream, new Encoding[] { Encoding.ASCII, Encoding.UTF8, Encoding.Unicode, Encoding.UTF32, Encoding.UTF7, Encoding.BigEndianUnicode }, new string[] { "ö", "ü", "ä", "Ö", "Ü", "Ä", "ß" }, 100);
        }


        /// <summary>
        /// Gibt das Encoding eine Datei zurück.
        /// </summary>
        /// <param name="stream">Der Stream der geprüft werden soll.</param>
        /// <returns>Das entsprechend gefundene Encoding. Wenn keins gefunden wurde, wird Encoding.Default zurückgegeben.</returns>
        public static Encoding DetectEncoding(FileStream stream, Encoding defaultEncoding)
        {
            return DetectEncoding(stream, new Encoding[] { Encoding.ASCII, Encoding.UTF8, Encoding.Unicode, Encoding.UTF32, Encoding.UTF7, Encoding.BigEndianUnicode }, new string[] { "ö", "ü", "ä", "Ö", "Ü", "Ä", "ß" }, 100, defaultEncoding);
        }



        /// <summary>
        /// Gibt das Encoding eine Datei zurück.
        /// </summary>
        /// <param name="stream">Der Stream der geprüft werden soll.</param>
        /// <param name="toTest">Die Encodings und Codepages, die getestet werden sollen.</param>
        /// <returns>Das entsprechend gefundene Encoding. Wenn keins gefunden wurde, wird Encoding.Default zurückgegeben.</returns>
        public static Encoding DetectEncoding(FileStream stream, Encoding[] toTest)
        {
            return DetectEncoding(stream, toTest, new string[] { "ö", "ü", "ä", "Ö", "Ü", "Ä", "ß" }, 100);
        }

        /// <summary>
        /// Gibt das Encoding eine Datei zurück.
        /// </summary>
        /// <param name="stream">Der Stream der geprüft werden soll.</param>
        /// <param name="toTest">Die Encodings und Codepages, die getestet werden sollen.</param>
        /// <param name="manuChars">Wenn kein BOM-Header angegeben wurde, welche Zeichen einzeln geprüft werden sollen, ob diese enthalten sind.</param>
        /// <returns>Das entsprechend gefundene Encoding. Wenn keins gefunden wurde, wird Encoding.Default zurückgegeben.</returns>
        public static Encoding DetectEncoding(FileStream stream, Encoding[] toTest, string[] manuChars)
        {
            return DetectEncoding(stream, toTest, manuChars, 100);
        }

        /// <summary>
        /// Gibt das Encoding eine Datei zurück.
        /// </summary>
        /// <param name="stream">Der Stream der geprüft werden soll.</param>
        /// <param name="toTest">Die Encodings und Codepages, die getestet werden sollen.</param>
        /// <param name="manuChars">Wenn kein BOM-Header angegeben wurde, welche Zeichen einzeln geprüft werden sollen, ob diese enthalten sind.</param>
        /// <param name="testLines">Die Anzahl der Zeilen, die geprüft werden, wenn kein BOM-Header angegeben wurde.</param>
        /// <returns>Das entsprechend gefundene Encoding. Wenn keins gefunden wurde, wird Encoding.Default zurückgegeben.</returns>
        public static Encoding DetectEncoding(FileStream stream, Encoding[] toTest, string[] manuChars, int testLines, Encoding defaultEncoding = null)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            if (toTest == null) throw new ArgumentNullException("toTest");
            if (manuChars == null) throw new ArgumentNullException("testChars");
            if (testLines < 0) throw new ArgumentOutOfRangeException("testLines");

            //Position zwischen Speichern
            long position = stream.Position;
            Encoding res = null;

            try
            {
#if DEBUG
                byte[] testdata = new byte[80];
                stream.Read(testdata, 0, testdata.Length);
#endif

                for (int fi = 0; fi < toTest.Length; fi++)
                {
                    stream.Position = 0;
                    byte[] preamp = toTest[fi].GetPreamble();
                    bool eq = false;

                    if (preamp.Length > 0)
                    {
                        for (int fm = 0; fm < preamp.Length; fm++)
                        {
                            eq = preamp[fm] == stream.ReadByte();
                            if (!eq) break;
                        }

                        if (eq)
                        {
                            res = toTest[fi];
                            break;
                        }
                    }
                }

                if (res == null)
                {

                    foreach (Encoding fEnc in toTest)
                    {
                        stream.Position = 0;

                        int curline = 0;
                        string line;

                        if (fEnc == Encoding.UTF8)
                        {
                        }

                        StreamReader rd = new StreamReader(stream, fEnc);
                        line = rd.ReadLine();
                        while (line != null && curline < testLines)
                        {
                            for (int fi = 0; fi < manuChars.Length; fi++)
                            {
                                if (line.Contains(manuChars[fi]))
                                {
                                    res = fEnc;
                                    break;
                                }
                            }

                            if (res != null)
                            {
                                break;
                            }

                            line = rd.ReadLine();
                            curline++;
                        }

                        if (res != null)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                stream.Position = 0;
            }

            if (res == null)
                res = defaultEncoding != null ? defaultEncoding : Encoding.Default;

            return res;
        }

        #endregion
    }
}

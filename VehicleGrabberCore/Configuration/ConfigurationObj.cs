using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Serialization;
using VehicleGrabberCore.Exporter;
using VehicleGrabberCore.Helper;
using VehicleGrabberCore.Logger;

namespace VehicleGrabberCore.Configuration
{
    public class ConfigurationObj
    {
        private VGCore Core = null;

        public ConfigurationObj(string configFile = null, VGCore core = null)
        {
            this.Core = core;
            this.SetDefaultSettings();

            if (!string.IsNullOrWhiteSpace(configFile))
            {
                this.LoadConfiguration(configFile);
            }

        }


        public ConfigurationObj()
        {
            this.SetDefaultSettings();

        }


        /// <summary>
        /// Initialize the values with default settings
        /// </summary>
        private void SetDefaultSettings()
        {
            this.LogDirectory = string.Empty;
            this.ShowLogInfo = true;
            this.ShowLogWarning = true;
            this.ShowLogError = true;
            this.ShowLogFatal = true;
            this.ShowLogDebug = true;

            // Export types
            this.ExportCSV = true;
            this.ExportMySQL = true;

            //CSV Export
            this.ExportDirectory = string.Empty;
            this.DefaultCSVDelimiter = 0;
            this.DefaultCSVFileExtension = "csv";
            this.DefaultCSVEncoding = "UTF-8";



            //MySQL Export
            this.SQLServer = string.Empty;
            this.SQLDataBase = string.Empty;
            this.SQLPort = 3306;
            this.SQLUser = string.Empty;
            this.SQLPassword = String.Empty;
            this.SQLSSLConnection = true;
        }



        /// <summary>
        /// Load configuration file and set values for current configuration
        /// </summary>
        /// <param name="configFile">string - configuration file</param>
        public void LoadConfiguration(string configFile)
        {
            try
            {
                if (File.Exists(configFile))
                {
                    XmlSerializer mySerializer = new XmlSerializer(typeof(ConfigurationObj));
                    FileStream myFileStream =
                        new FileStream(configFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    ConfigurationObj conf = (ConfigurationObj)mySerializer.Deserialize(myFileStream);

                    myFileStream.Close();

                    this.LogDirectory = conf.LogDirectory;
                    this.ShowLogInfo = conf.ShowLogInfo;
                    this.ShowLogWarning = conf.ShowLogWarning;
                    this.ShowLogError = conf.ShowLogError;
                    this.ShowLogFatal = conf.ShowLogFatal;
                    this.ShowLogDebug = conf.ShowLogDebug;

                    //Export types
                    this.ExportCSV = conf.ExportCSV;
                    this.ExportMySQL = conf.ExportMySQL;

                    //CSV Export
                    this.ExportDirectory = conf.ExportDirectory;
                    this.DefaultCSVDelimiter = conf.DefaultCSVDelimiter;
                    this.DefaultCSVFileExtension = conf.DefaultCSVFileExtension;
                    this.DefaultCSVEncoding = conf.DefaultCSVEncoding;

                    //MySQL Export
                    this.SQLServer = conf.SQLServer;
                    this.SQLDataBase = conf.SQLDataBase;
                    this.SQLPort = conf.SQLPort;
                    this.SQLUser = conf.SQLUser;                    
                    this.SQLSSLConnection = conf.SQLSSLConnection;
                    this.SQLPassword = this.SQLPassword = conf.SQLPassword != string.Empty
                        ? StringCipher.Decrypt(conf.SQLPassword, this.Core.GetCipherPW())
                        : string.Empty;


                   

                    LogItem logItem = new LogItem(string.Format("Load configuration file. '{0}'", configFile), DateTime.Now, LogLevel.INFO);
                    this.Core.preLogItems.Capacity++;
                    this.Core.preLogItems.Add(logItem);
                }
                else
                {
                    LogItem logItem = new LogItem(string.Format("File not found: '{0}'", configFile), DateTime.Now, LogLevel.ERROR);
                    this.Core.preLogItems.Capacity++;
                    this.Core.preLogItems.Add(logItem);
                }
            }
            catch (Exception ex)
            {

                LogItem logItem = new LogItem(ex.Message, DateTime.Now, LogLevel.ERROR);
                this.Core.preLogItems.Capacity++;
                this.Core.preLogItems.Add(logItem);

                //this.Core.Log.Error(string.Format("Error on loading configuration file. '{0}'", ex.Message));

            }
        }

        public void SaveConfigurationToFile(string fileName)
        {
            try
            {
                // encrypt password before storing the config to file
                this.SQLPassword = StringCipher.Encrypt(this.SQLPassword, this.Core.GetCipherPW());
                XmlSerializer mySerializer = new XmlSerializer(typeof(ConfigurationObj));
                StreamWriter myWriter = new StreamWriter(fileName);
                mySerializer.Serialize(myWriter, this);
                myWriter.Flush();
                myWriter.Close();
            }
            catch (Exception ex)
            {
                this.Core.Log.Error(string.Format("Error on saving configuration file: {0}", ex.Message));
            }
        }



       
        //Logger
        public string LogDirectory { get; set; }

        public bool ShowLogInfo { get; set; }

        public bool ShowLogWarning { get; set; }

        public bool ShowLogError { get; set; }

        public bool ShowLogFatal { get; set; }

        public bool ShowLogDebug { get; set; }

        //Export types
        public bool ExportCSV { get; set; }

        public bool ExportMySQL { get; set; }

        //CSV Export
        public string ExportDirectory { get; set; }

        public int DefaultCSVDelimiter { get; set; }

        public string DefaultCSVFileExtension { get; set; }

        public string DefaultCSVEncoding { get; set; }


        //MySQL Export
        public string SQLServer { get; set; }

        public UInt32 SQLPort { get; set; }

        public string SQLDataBase { get; set; }

        public bool SQLSSLConnection { get; set; }

        public string SQLCatalogRefs { get; set; }//WOTAG MIS-Gate

        public string SQLCatalogLCC { get; set; }

        public string SQLUser { get; set; }

        public string SQLPassword { get; set; }

    }
}

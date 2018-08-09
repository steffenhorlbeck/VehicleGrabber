using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using VehicleGrabberCore.Configuration;
using VehicleGrabberCore.DataObjects;
using VehicleGrabberCore.Exporter;
using VehicleGrabberCore.Importer;
using VehicleGrabberCore.Logger;

namespace VehicleGrabberCore
{
    public class VGCore
    {
        public const string COMPANY_DIR = "MEGRASO";
        public const string APPNAME = "VehicleGrabber";
        public const string DEFAULT_CONFIG_FILE_NAME = "VehicleGrabber.config";


        public ConfigurationObj Conf = null;
        public Logger.Logger Log;

        private const string CipherPW = "#MorgAne0815?";

       

        public ImporterBase Importer = null;

        public List<LogItem> preLogItems = null;

        public Action<string> TriggerLogLine { get; set; }


        public VGCore(string configFile = null)
        {
            this.preLogItems = new List<LogItem>();
            this.Conf = new ConfigurationObj(configFile, this);

            this.Log = new Logger.Logger(this.Conf, false);

            // after logger is initialized we need to put the log Items from preLogItems to it
            foreach (LogItem item in preLogItems)
            {
                this.Log.LogItems.Add(item);
            }
            preLogItems.Clear();
        }

        /// <summary>
        /// Get the internal password for encrypting strings
        /// </summary>
        /// <returns>
        /// string - the password string
        /// </returns>
        public string GetCipherPW()
        {
            return CipherPW;
        }

        public void Import(int importType = 0, BackgroundWorker bw = null)
        {
            if (importType == (int)ImporterBase.ImporterType.ADAC)
            {
                Importer = new ADACImporter();
            } else if (importType == (int)ImporterBase.ImporterType.AUTOMOBILIO)
            {
                Importer = new AutomobilioImporter();
            }

            Importer.Core = this;

            Importer.StartImport(bw);
        }



        public void ExportToCSV(BackgroundWorker bw = null)
        {
            CSVExporter exporter = new CSVExporter(Importer.MakersList, Importer.modelsList, Importer.modelTypesList, Importer.carDetailsList);
            exporter.Core = this;

            exporter.ExportModels();
            if (bw != null) { bw.ReportProgress(10); }
            exporter.ExportModelTypes();
            if (bw != null) { bw.ReportProgress(25); }
            exporter.ExportCarDetails();
            if (bw != null) { bw.ReportProgress(100); }
        }



        public void ExportToMySQL(BackgroundWorker bw = null)
        {
            string server = this.Conf.SQLServer;
            string db = this.Conf.SQLDataBase;
            string user = this.Conf.SQLUser;
            string pw = this.Conf.SQLPassword;
            long port = this.Conf.SQLPort;
            bool ssl = this.Conf.SQLSSLConnection;

            MySQLExporter exporter = new MySQLExporter(this, Importer.MakersList, Importer.modelsList, Importer.modelTypesList, Importer.carDetailsList, server, db, ssl, port, user, pw);

            // try to connect and continue on success
            if (exporter.OpenConnection())
            {
                exporter.CloseConnection();

                exporter.HandleMakers();
                if (bw != null)
                {
                    bw.ReportProgress(10);
                }

                exporter.HandleModels();
                if (bw != null)
                {
                    bw.ReportProgress(20);
                }

                exporter.HandleModelTypes();
                if (bw != null)
                {
                    bw.ReportProgress(30);
                }

                exporter.HandleCarDetails();
                if (bw != null)
                {
                    bw.ReportProgress(100);
                }
            }
        }

        public string GetPageContent()
        {
            string result = string.Empty;

            if(Importer != null)
            {
                result = Importer.GetPageContent();
            }

            return result;
        }

        public string GetBaseUrl()
        {
            string result = string.Empty;

            if (Importer != null)
            {
                result = Importer.GetBaseUrl();
            }

            return result;
        }

        public void UpdateTriggerLogLine()
        {
            this.Log.TriggerLogLine = this.TriggerLogLine;
        }
    }
}

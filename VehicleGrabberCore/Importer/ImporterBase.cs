using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using VehicleGrabberCore.DataObjects;
using VehicleGrabberCore.Exporter;

namespace VehicleGrabberCore.Importer
{
    public abstract class ImporterBase
    {
        protected string baseUrl = "http://automobilio.info";
        protected string baseUrlLang = "/de/";

        public List<MakerObj> MakersList = new List<MakerObj>();
        public List<ModelObj> modelsList = new List<ModelObj>();
        public List<ModelTypeObj> modelTypesList = new List<ModelTypeObj>();
        public List<CarDetailsObj> carDetailsList = new List<CarDetailsObj>();

        public VGCore Core { get; set; }

        public long RecLimit { get; set; }

        public enum ImporterType
        {
            ADAC,
            AUTOMOBILIO
        };

            

        protected ImporterBase(VGCore core)
        {
            this.Core = core;
            this.RecLimit = this.Core.Conf.RecordLimit;
        }

        protected ImporterBase()
        {
            this.RecLimit = this.Core.Conf.RecordLimit;
        }


        public abstract void StartImport(BackgroundWorker bw = null);

        public abstract string GetPageContent();

        public abstract string GetBaseUrl();

        protected bool IsLimited(long cnt)
        {
            if (this.Core.Conf.RecordLimit == 0)
            {
                return false;
            }
            else
            {
                if (cnt > this.Core.Conf.RecordLimit)
                {
                    return true;
                }
            }

            return false;
        }

        protected string DownloadModelImage(string imgUrl)
        {
            string result = string.Empty;
            try
            {
                using (WebClient client = new WebClient())
                {
                    string url = string.Format("{0}{1}", this.baseUrl, imgUrl);
                    string fileName = Path.Combine(CSVExporter.ROOT, CSVExporter.MODELS,
                        imgUrl.Substring(imgUrl.LastIndexOf("/") + 1));
                    if (!File.Exists(fileName))
                    {
                        client.DownloadFile(new Uri(url), fileName);
                    }

                    result = fileName;
                }

                return result;
            }
            catch (Exception ex)
            {
                if (this.Core != null && this.Core.Log != null)
                {
                    this.Core.Log.Error(string.Format("ImporterBase::DownloadImageFile : {0}", ex.Message));
                }
                else
                {
                    throw new Exception("ImporterBase::DownloadImageFile", ex);
                }

                return result;
            }
        }


        protected string DownloadMakerImage(string imgUrl)
        {
            string result = string.Empty;
            try
            {
                using (WebClient client = new WebClient())
                {
                    string url = string.Format("{0}{1}", this.baseUrl, imgUrl);
                    string fileName = Path.Combine(CSVExporter.ROOT, CSVExporter.MAKERS,
                        imgUrl.Substring(imgUrl.LastIndexOf("/") + 1));
                    if (!File.Exists(fileName))
                    {
                        client.DownloadFile(new Uri(url), fileName);
                    }

                    result = fileName;
                }

                return result;
            }
            catch (Exception ex)
            {
                if (this.Core != null && this.Core.Log != null)
                {
                    this.Core.Log.Error(string.Format("ImporterBase::DownloadMakerImage : {0}", ex.Message));
                }
                else
                {
                    throw new Exception("ImporterBase::DownloadMakerImage", ex);
                }

                return result;
            }
        }

        #region protected void GetContent(string url = "")

        protected string GetContent(string url = "")
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                url = string.Format("{0}{1}", this.baseUrl, this.baseUrlLang);
            }

            // Create web client.
            WebClient client = new WebClient();
            return client.DownloadString(url);
        }

        #endregion
    }
}

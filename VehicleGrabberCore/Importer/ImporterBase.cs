using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using HtmlAgilityPack;
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
            AUTOMOBILIO,
            ADAC_TYPEDB,
            ADAC_CURRENTMAKER
        };

            

        protected ImporterBase(VGCore core)
        {
            this.Core = core;
            this.RecLimit = this.Core.Conf.RecordLimit;
        }



        public abstract void StartImport(BackgroundWorker bw = null, string content = "");

        public abstract string GetPageContent();

        public abstract string GetBaseUrl();

        public abstract string GetCatalogUrl();

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
            string url = string.Empty;
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;

                    url = string.Format("{0}{1}", this.baseUrl, imgUrl);
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
                    this.Core.Log.Error(string.Format("ImporterBase::DownloadImageFile : {0} (URL:{1})", ex.Message, url));
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
            string url = string.Empty;
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;

                    url = string.Format("{0}{1}", this.baseUrl, imgUrl);
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
                    this.Core.Log.Error(string.Format("ImporterBase::DownloadMakerImage : {0} (URL:{1})", ex.Message, url));
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
            client.Encoding = System.Text.Encoding.UTF8;

            
            var web = new HtmlWeb();
            var doc = web.Load(url);
            return doc.Text;
            


            //return GetAllCurrentAndOldModels(url);

            //return client.DownloadString(url);


        }

        #endregion


        /*

        protected string GetAllCurrentAndOldModels(string url = "")
        {
            System.Windows.Forms.WebBrowser browser = new WebBrowser();


            if (string.IsNullOrWhiteSpace(url))
            {
                url = string.Format("{0}{1}", this.baseUrl, this.baseUrlLang);
            }

            browser.Navigate(url);

            HtmlElementCollection theElementCollection = browser.Document.GetElementsByTagName("input");
            foreach (HtmlElement curElement in theElementCollection)
            {
                if (curElement.GetAttribute("value").Equals("radioAllModels"))
                {
                    curElement.InvokeMember("click");
                    break;
                    // Javascript has a click method for you need to invoke on button and hyperlink elements.
                }
            }

            HtmlElementCollection theElementCollection1 = browser.Document.GetElementsByTagName("a");

            foreach (HtmlElement curElement in theElementCollection1)
            {
                if (curElement.GetAttribute("href").Contains("linkReiterAlphabetisch"))
                {
                    curElement.InvokeMember("click");
                    break;
                }
            }


            return browser.DocumentText;
        }
        */




        public abstract void SetPageContent(string content);
    }
}

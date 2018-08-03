using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using VehicleGrabberCore.DataObjects;
using VehicleGrabberCore.Exporter;


public abstract class ImporterBase
{
    protected string baseUrl = "http://automobilio.info";
    protected string baseUrlLang = "/de/";

    public List<MakerObj> MakersList = new List<MakerObj>();
    public List<ModelObj> modelsList = new List<ModelObj>();
    public List<ModelTypeObj> modelTypesList = new List<ModelTypeObj>();
    public List<CarDetailsObj> carDetailsList = new List<CarDetailsObj>();

    public enum ImporterType
    {
        ADAC,
        AUTOMOBILIO
    };



    public ImporterBase()
    {
    }



    public abstract void StartImport();

    public abstract string GetPageContent();

    public abstract string GetBaseUrl();

    protected string DownloadModelImage(string imgUrl)
    {
        string result = string.Empty;
        try
        {
            using (WebClient client = new WebClient())
            {
                string url = string.Format("{0}{1}", this.baseUrl, imgUrl);
                string fileName = Path.Combine(CSVExporter.ROOT, CSVExporter.MODELS, imgUrl.Substring(imgUrl.LastIndexOf("/") + 1));
                if (!File.Exists(fileName))
                {
                    client.DownloadFileAsync(new Uri(url), fileName);
                }
                result = fileName;
            }
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Importer::DownloadImageFile", ex);
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
                string fileName = Path.Combine(CSVExporter.ROOT, CSVExporter.MAKERS, imgUrl.Substring(imgUrl.LastIndexOf("/") + 1));
                if (!File.Exists(fileName))
                {
                    client.DownloadFileAsync(new Uri(url), fileName);
                }
                result = fileName;
            }
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Importer::DownloadMakerImage", ex);
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


using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using VehicleGrabberCore.DataObjects;


namespace VehicleGrabberCore.Importer
{
    public class AutomobilioImporter : ImporterBase
    {

        public AutomobilioImporter() : base()
        {
            this.baseUrl = "http://automobilio.info";
            this.baseUrlLang = "/de/";
        }

        private string pageContent = string.Empty;


        public override string GetBaseUrl()
        {
            return this.baseUrl;
        }

        public override string GetPageContent()
        {
            return this.pageContent;
        }

        public override void StartImport(BackgroundWorker bw = null)
        {
            try
            {
                string url = string.Format("{0}{1}", this.baseUrl, this.baseUrlLang);
                this.pageContent = GetContent(url);
                GetMakers();
                if (bw != null) { bw.ReportProgress(10); }
                GetModels();
                if (bw != null) { bw.ReportProgress(25); }

                if (bw != null) { bw.ReportProgress(100); }
            }
            catch (Exception ex)
            {
                throw new Exception("AutomobilioImporter::ReadPageContent", ex);
            }
        }





        private void GetModels()
        {
            foreach (MakerObj obj in this.MakersList)
            {
                string modelsUrl = string.Format("{0}{1}", this.baseUrl, obj.MakerUrlPath);
                string modelsContent = GetContent(modelsUrl);

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(modelsContent);

                HtmlNodeCollection model_div = null;
                if (htmlDoc.DocumentNode != null && htmlDoc.ParseErrors != null && !htmlDoc.ParseErrors.Any())
                {
                    model_div = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"content\"]/div/div[4]/div[1]/ul[1]/li");


                }
                //sleep(1000);

                if (model_div != null)
                {
                    foreach (HtmlNode modelNode in model_div)
                    {
                        ModelObj modelObj = new ModelObj();

                        modelObj.ModelID = this.modelsList.Count + 1;
                        // get Model name
                        modelObj.ModelName = modelNode.ChildNodes[3].ChildNodes[1].InnerText;

                        // get model link
                        modelObj.ModelUrlPath = modelNode.ChildNodes[3].ChildNodes[1].Attributes
                            .AttributesWithName("href").First().Value;

                        // get image url
                        modelObj.ModelThumbUrl = modelNode.ChildNodes[1].ChildNodes[0].Attributes
                            .AttributesWithName("src").First().Value;

                        modelObj.MakerName = obj.MakerName;
                        modelObj.MakerUrlPath = obj.MakerUrlPath;
                        modelObj.MakerLogoUrl = obj.MakerLogoUrl;
                        modelObj.MakerLogoBase64 = obj.MakerLogoBase64;

                        if (this.modelsList.Find(x =>
                                x.MakerName.ToUpper().Equals(obj.MakerName.ToUpper()) &&
                                x.ModelName.ToUpper().Equals(modelObj.ModelName.ToUpper())) == null)
                        {
                            this.modelsList.Add(modelObj);
                            DownloadModelImage(modelObj.ModelThumbUrl);
                            GetModelTypes(modelObj);
                        }


                    }
                }
            }
        }




        private void GetModelTypes(ModelObj modelObj)
        {
            string modelTypesUrl = string.Format("{0}{1}", this.baseUrl, modelObj.ModelUrlPath);
            string modelsContent = GetContent(modelTypesUrl);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(modelsContent);

            HtmlNodeCollection types_div = null;
            if (htmlDoc.DocumentNode != null && htmlDoc.ParseErrors != null && !htmlDoc.ParseErrors.Any())
            {
                types_div = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"modelis\"]/table");
            }

            if (types_div != null)
            {
                foreach (var node in types_div.First().ChildNodes)
                {

                    ModelTypeObj typeObj = new ModelTypeObj();

                    typeObj.ModelTypeID = modelTypesList.Count + 1;
                    typeObj.ModelID = modelObj.ModelID;
                    typeObj.ModelTypeName = node.ChildNodes[0].InnerText;
                    typeObj.ModelTypeCubic = node.ChildNodes[1].InnerText
                        .Substring(0, node.ChildNodes[1].InnerText.IndexOf("cm3"));
                    typeObj.ModelTypeFuel = node.ChildNodes[2].InnerText;
                    typeObj.ModelTypePower = node.ChildNodes[3].InnerText;
                    typeObj.ModelTypeTank = node.ChildNodes[4].InnerText;
                    typeObj.ModelTypeFromYear = node.ChildNodes[5].InnerText;
                    typeObj.ModelTypeToYear = node.ChildNodes[6].InnerText;

                    string value = node.ChildNodes[0].Attributes.AttributesWithName("onclick").First().Value;
                    value = value.Substring(value.IndexOf("'") + 1, value.LastIndexOf("'") - value.IndexOf("'") - 1);

                    typeObj.ModelTypeDetailsUrl = value;

                    modelTypesList.Add(typeObj);
                }
            }
        }

        private void GetMakers()
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(this.pageContent);

            HtmlNodeCollection Maker_div = null;
            if (htmlDoc.DocumentNode != null && htmlDoc.ParseErrors != null && !htmlDoc.ParseErrors.Any())
            {
                Maker_div = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"marke\"]");

            }

            if (Maker_div != null)
            {
                htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(Maker_div.First().InnerHtml);
                var MakerNodes = htmlDoc.DocumentNode.SelectNodes("//*//td");

                foreach (var node in MakerNodes
                ) //.Zip(descriptions, (n, d) => new MakerClass { MakerName = n.InnerText, MakerUrlPath = d.InnerText }))
                {
                    MakerObj MakerObj = new MakerObj();

                    MakerObj.MakerName = node.InnerText;
                    string value = node.Attributes.AttributesWithName("onclick").First().Value;
                    value = value.Substring(value.IndexOf("'") + 1, value.LastIndexOf("'") - value.IndexOf("'") - 1);
                    MakerObj.MakerUrlPath = value;

                    this.MakersList.Add(MakerObj);
                }
            }
        }
    }
}
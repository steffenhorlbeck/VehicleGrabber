using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
//using System.Windows.Forms;
using VehicleGrabberCore.DataObjects;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace VehicleGrabberCore.Importer
{
    class ADACImportCurrentMakerModels : ImporterBase
    {
        private string startPath = "/infotestrat/autodatenbank/autokatalog/default.aspx";
        private string parameters = "ctl00$ctl00$cphContentRow$cphContent$wucNFBAutokatalogMarken1$rb123=radioAllModels";

        private BackgroundWorker bw;
        private string pageContent = string.Empty;

        public ADACImportCurrentMakerModels(VGCore core) : base(core)
        {
            this.CurrentType = (int)ImporterBase.ImporterType.ADAC_CURRENTMAKER;

            this.baseUrl = "https://www.adac.de";
            this.baseUrlLang = string.Empty;
        }

        public override string GetBaseUrl()
        {
            return this.baseUrl;
        }


        public override string GetCatalogUrl()
        {
            return string.Format("{0}{1}{2}?{3}", this.baseUrl, this.startPath, this.baseUrlLang, this.parameters);
        }

        public override void SetPageContent(string content)
        {
            this.pageContent = content;
        }

        public override string GetPageContent()
        {
            return this.pageContent;
        }

        public override void StartImport(BackgroundWorker bw = null, string content = "")
        {
            string url = string.Empty;
            try
            {
                this.EmptyLists();

                //this.pageContent = content;

                GetModels();
                if (bw != null) { bw.ReportProgress(25); }
                GetCarDetails();
                if (bw != null) { bw.ReportProgress(100); }
            }
            catch (Exception ex)
            {
                if (this.Core != null && this.Core.Log != null)
                {
                    this.Core.Log.Error(string.Format("ADACImporter::ReadPageContent : {0} (URL:{1})", ex.Message, url));
                }
                else
                {
                    throw new Exception("ADACImporter::ReadPageContent", ex);
                }

            }
        }


        private void GetMakers()
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(this.pageContent);

            HtmlNodeCollection Maker_div = null;
            if (htmlDoc.DocumentNode != null)
            {
                Maker_div = htmlDoc.DocumentNode.SelectNodes(
                    "//*[@id=\"ctl00_ctl00_cphContentRow_cphContent_wucNFBAutokatalogMarken1_updatePanelMarken\"]/div[2]/div[2]/ul");

            }

            if (Maker_div != null)
            {
                htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(Maker_div.First().InnerHtml);
                var MakerNodes = htmlDoc.DocumentNode.SelectNodes("//li");

                foreach (var node in MakerNodes
                ) //.Zip(descriptions, (n, d) => new MakerClass { MakerName = n.InnerText, MakerUrlPath = d.InnerText }))
                {
                    if (this.Core.Conf.MakerName.Trim().Equals(string.Empty) ||
                       this.Core.Conf.MakerName.Trim().Equals("*") ||
                       this.Core.Conf.MakerName.ToUpper().Equals(CleanNameString(node.InnerText.Trim().ToUpper())))
                        try
                        {
                            MakerObj MakerObj = new MakerObj();

                            MakerObj.MakerName = CleanNameString(node.InnerText.Trim());


                            //MakerObj.MakerLogoUrl = node.SelectSingleNode("/li[1]/a[1]/div[1]/img[1]").Attributes //"//*[@id=\"  //
                            //    .AttributesWithName("src").First().Value;

                            MakerObj.MakerLogoUrl = node.FirstChild.ChildNodes[1].FirstChild.ChildAttributes("src")
                                .First().Value;
                            //    .AttributesWithName("src").First().Value;

                            //Maker name
                            //   ChildNodes[1].ChildNodes[0].InnerText;

                            string localImgFile = DownloadMakerImage(MakerObj.MakerLogoUrl);
                            MakerObj.MakerLogoLocalFile = localImgFile;

                            //string value = node.Attributes.AttributesWithName("onclick").First().Value;
                            //value = value.Substring(value.IndexOf("'") + 1, value.LastIndexOf("'") - value.IndexOf("'") - 1);
                            //MakerObj.MakerUrlPath = node.SelectSingleNode("/li[1]/a[1]").Attributes.AttributesWithName("href")
                            //    .First().Value;

                            MakerObj.MakerUrlPath = node.FirstChild.Attributes.AttributesWithName("href")
                                .First().Value;

                            this.MakersList.Add(MakerObj);
                            System.Threading.Thread.Sleep(100);
                        }
                        catch (Exception ex)
                        {
                            if (this.Core != null && this.Core.Log != null)
                            {
                                this.Core.Log.Error(string.Format("ADACImporter::GetMakers : {0}", ex.Message));
                            }
                            else
                            {
                                throw new Exception("ADACImporter::GetMakers", ex);
                            }
                        }
                }

                if (this.Core != null && this.Core.Log != null)
                {
                    this.Core.Log.Info(string.Format("{0} Maker Records imported.", MakersList.Count));
                }

            }
        }

        private static string CleanNameString(string name)
        {
            string retval = name.Replace(Environment.NewLine, "");

            while (retval.Contains("  "))
            {
                retval = retval.Replace("  ", " ");
            }
            return retval;
        }




        private void GetModels()
        {
            int limit_cnt = 0;
//            foreach (MakerObj obj in this.MakersList)
//            {
//                limit_cnt++;

                
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(this.pageContent);

                HtmlNodeCollection model_div = null;
                if (htmlDoc.DocumentNode != null)
                {
                    model_div = htmlDoc.DocumentNode.SelectNodes(
                        "//*[@id=\"ctl00_ctl00_cphContentRow_cphContent_wucNFBAutokatalogBaureihe1_updatePanelBaureihe\"]/div[2]/div[2]/a");

                    if (model_div == null)
                    {
                        model_div = htmlDoc.DocumentNode.SelectNodes(
                            "//*[@id=\"ctl00_ctl00_cphContentRow_cphContent_wucNFBAutokatalogBaureihe1_updatePanelBaureihe\"]/div[2]/div[2]");

                    }

                }




                //sleep(1000);

                if (model_div != null)
                {
                    int limit_cnt1 = 0;
                    foreach (HtmlNode modelNode in model_div)
                    {
                        limit_cnt1++;
                        ModelObj modelObj = new ModelObj();

                        try
                        {
                            modelObj.ModelID = this.modelsList.Count + 1;
                            modelObj.ModelName = CleanNameString(modelNode.ChildNodes[2].InnerText.Trim());
                            modelObj.ModelUrlPath = modelNode.Attributes.AttributesWithName("href").First().Value;
                            modelObj.ModelThumbUrl = modelNode.ChildNodes[0].ChildNodes[0].Attributes
                                .AttributesWithName("src")
                                .First().Value;

//                            modelObj.MakerName = obj.MakerName.Trim();
//                            modelObj.MakerUrlPath = obj.MakerUrlPath;
//                            modelObj.MakerLogoUrl = obj.MakerLogoUrl;
//                            modelObj.MakerLogoBase64 = obj.MakerLogoBase64;




                            if (this.modelsList.Find(x =>
//                                    x.MakerName.ToUpper().Equals(obj.MakerName.ToUpper()) &&
                                    x.ModelName.ToUpper().Equals(modelObj.ModelName.ToUpper())) == null)
                            {
                                this.modelsList.Add(modelObj);
                                string localImgFile = DownloadModelImage(modelObj.ModelThumbUrl);
                                modelObj.ModelLocalFile = localImgFile;
                                GetModelTypes(modelObj);
                            }

                            System.Threading.Thread.Sleep(50);

                            //DEBUG: Break after x number of models

                            if (this.IsLimited(limit_cnt1))
                            {
                                break;
                            }


                        }
                        catch (Exception ex)
                        {
                            if (this.Core != null && this.Core.Log != null)
                            {
                                this.Core.Log.Error(string.Format("ADACImporter::GetModels : {0}", ex.Message));
                            }
                            else
                            {
                                throw new Exception("ADACImporter::GetModels", ex);
                            }
                        }
                    }
                }

/*
                if (this.IsLimited(limit_cnt))
                {
                    break;
                }
*/
//            }

            if (this.Core != null && this.Core.Log != null)
            {
                this.Core.Log.Info(string.Format("{0} Model Records imported.", modelsList.Count));
                this.Core.Log.Info(string.Format("{0} ModelType Records imported.", modelTypesList.Count));
            }

        }

        private void GetModelTypes(ModelObj modelObj)
        {
            string modelTypesUrl = string.Format("{0}{1}", this.baseUrl, modelObj.ModelUrlPath);
            string modelsContent = GetContent(modelTypesUrl);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(modelsContent);

            HtmlNodeCollection types_div = null;
            if (htmlDoc.DocumentNode != null)
            {
                types_div = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"car_db_select_hits\"]/tbody");
            }

            if (types_div != null)
            {
                int limit_cnt = 0;
                foreach (var node in types_div.First().ChildNodes)
                {
                    try
                    {
                        if (node.Name.ToLower().Equals("tr"))
                        {
                            limit_cnt++;

                            ModelTypeObj typeObj = new ModelTypeObj();

                            typeObj.ModelTypeID = modelTypesList.Count + 1;
                            typeObj.ModelID = modelObj.ModelID;
                            typeObj.MakerName = modelObj.MakerName;
                            typeObj.ModelName = modelObj.ModelName;
                            typeObj.ModelTypeDetailsUrl = node.ChildNodes[3].ChildNodes[1].Attributes
                                .AttributesWithName("href")
                                .First().Value;
                            typeObj.ModelTypeName = node.ChildNodes[5].InnerText.Trim();
                            typeObj.ModelTypeChassis = node.ChildNodes[7].InnerText.Trim();
                            typeObj.ModelTypeDoors = Convert.ToInt32(node.ChildNodes[9].InnerText.Trim());
                            typeObj.ModelTypeFuel = node.ChildNodes[11].InnerText.Trim();
                            typeObj.ModelTypePower = node.ChildNodes[13].InnerText.Trim();
                            typeObj.ModelTypeCubic = node.ChildNodes[17].InnerText.Trim();



                            //link to details page

                            //Type Name
                            //Chassis
                            //Doors
                            //Fuel
                            //KW


                            modelTypesList.Add(typeObj);

                            //DEBUG: Break after x number of model types
                            if (this.IsLimited(limit_cnt))
                            {
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (this.Core != null && this.Core.Log != null)
                        {
                            this.Core.Log.Error(string.Format("ADACImporter::GetModelTypes : {0}", ex.Message));
                        }
                        else
                        {
                            throw new Exception("ADACImporter::GetModelTypes", ex);
                        }
                    }
                }
            }
        }


        private void GetCarDetails()
        {


            foreach (ModelTypeObj type in modelTypesList)
            {
                string modelDetailsUrl = string.Format("{0}{1}", this.baseUrl, type.ModelTypeDetailsUrl);
                try
                {
                    string carContent = GetContent(modelDetailsUrl);

                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(carContent);

                    HtmlNodeCollection cars_div = null;
                    if (htmlDoc.DocumentNode != null)
                    {
                        cars_div = htmlDoc.DocumentNode.SelectNodes(
                            "//*[@id=\"ctl00_ctl00_cphContentRow_cphContent_wucNFBAutokatalogDetail1_ctl01_updatePanelDetail\"]/div[2]/div[3]/div[1]/table/tbody");
                    }

                    if (cars_div != null)
                    {
                        CarDetailsObj carObj = new CarDetailsObj();

                        carObj.ModelTypeID = type.ModelTypeID;
                        carObj.ModelID = type.ModelID;
                        carObj.Maker = HttpUtility.HtmlDecode(cars_div.First().ChildNodes[1].ChildNodes[1].InnerText);
                        carObj.Model = HttpUtility.HtmlDecode(cars_div.First().ChildNodes[2].ChildNodes[1].InnerText);
                        carObj.Type = HttpUtility.HtmlDecode(cars_div.First().ChildNodes[3].ChildNodes[1].InnerText);
                        carObj.Series = HttpUtility.HtmlDecode(cars_div.First().ChildNodes[4].ChildNodes[1].InnerText);
                        carObj.ModelTypeName = type.ModelTypeName;
                        carObj.InternalClassName =
                            HttpUtility.HtmlDecode(cars_div.First().ChildNodes[5].ChildNodes[1].InnerText);
                        carObj.ModelStart =
                            HttpUtility.HtmlDecode(cars_div.First().ChildNodes[6].ChildNodes[1].InnerText);
                        carObj.ModelEnd =
                            HttpUtility.HtmlDecode(cars_div.First().ChildNodes[7].ChildNodes[1].InnerText);
                        carObj.SeriesStart =
                            HttpUtility.HtmlDecode(cars_div.First().ChildNodes[8].ChildNodes[1].InnerText);
                        carObj.SeriesEnd =
                            HttpUtility.HtmlDecode(cars_div.First().ChildNodes[9].ChildNodes[1].InnerText);
                        carObj.HSN = HttpUtility.HtmlDecode(cars_div.First().ChildNodes[10].ChildNodes[1].InnerText);
                        carObj.TSN = HttpUtility.HtmlDecode(cars_div.First().ChildNodes[11].ChildNodes[1].InnerText);
                        carObj.TSN2 = HttpUtility.HtmlDecode(cars_div.First().ChildNodes[12].ChildNodes[1].InnerText);
                        carObj.CarTax = HttpUtility.HtmlDecode(cars_div.First().ChildNodes[13].ChildNodes[1].InnerText);
                        carObj.CO2Class =
                            HttpUtility.HtmlDecode(cars_div.First().ChildNodes[14].ChildNodes[1].InnerText);
                        carObj.BasePrice =
                            HttpUtility.HtmlDecode(cars_div.First().ChildNodes[15].ChildNodes[1].InnerText);




                        // Motor & Antrieb
                        HtmlNodeCollection carEngine_div = null;
                        carEngine_div = htmlDoc.DocumentNode.SelectNodes(
                            "//*[@id=\"ctl00_ctl00_cphContentRow_cphContent_wucNFBAutokatalogDetail1_ctl01_updatePanelDetail\"]/div[2]/div[3]/div[2]/table/tbody");

                        if (carEngine_div != null)
                        {
                            carObj.EngineType =
                                HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[0].ChildNodes[1].InnerText);
                            carObj.Fuel =
                                HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[1].ChildNodes[1].InnerText);
                            carObj.Fuel2 =
                                HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[2].ChildNodes[1].InnerText);
                            carObj.EmissionControl =
                                HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[3].ChildNodes[1].InnerText);
                            carObj.EngineDesign =
                                HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[4].ChildNodes[1].InnerText);
                            carObj.Cylinder =
                                Convert.ToInt32(carEngine_div.First().ChildNodes[5].ChildNodes[1].InnerText);
                            carObj.FuelType =
                                HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[6].ChildNodes[1].InnerText);
                            carObj.Charge =
                                HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[7].ChildNodes[1].InnerText);
                            carObj.Valves =
                                Convert.ToInt32(carEngine_div.First().ChildNodes[8].ChildNodes[1].InnerText);
                            carObj.Cubic =
                                HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[9].ChildNodes[1].InnerText);
                            carObj.PowerKW =
                                Convert.ToInt32(carEngine_div.First().ChildNodes[10].ChildNodes[1].InnerText);
                            carObj.PowerPS =
                                Convert.ToInt32(carEngine_div.First().ChildNodes[11].ChildNodes[1].InnerText);
                            carObj.MaxPower =
                                HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[12].ChildNodes[1].InnerText);
                            carObj.TurningMoment =
                                HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[13].ChildNodes[1].InnerText);
                            carObj.MaxTurningMoment =
                                HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[14].ChildNodes[1].InnerText);
                            carObj.TypeOfDrive =
                                HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[15].ChildNodes[1].InnerText);
                            carObj.Gearing =
                                HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[16].ChildNodes[1].InnerText);
                            carObj.Gears =
                                Convert.ToInt32(carEngine_div.First().ChildNodes[17].ChildNodes[1].InnerText);
                            carObj.StartStopAutomatic =
                                HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[18].ChildNodes[1].InnerText);
                            carObj.EmissionClass =
                                HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[20].ChildNodes[1].InnerText);
                        }


                        // Maße & Gewicht
                        HtmlNodeCollection carDimensions_div = null;
                        carDimensions_div = htmlDoc.DocumentNode.SelectNodes(
                            "//*[@id=\"ctl00_ctl00_cphContentRow_cphContent_wucNFBAutokatalogDetail1_ctl01_updatePanelDetail\"]/div[2]/div[3]/div[3]/table/tbody");

                        if (carDimensions_div != null)
                        {
                            carObj.Length =
                                HttpUtility.HtmlDecode(carDimensions_div.First().ChildNodes[0].ChildNodes[1].InnerText);
                            carObj.Width =
                                HttpUtility.HtmlDecode(carDimensions_div.First().ChildNodes[1].ChildNodes[1].InnerText);
                            carObj.Height =
                                HttpUtility.HtmlDecode(carDimensions_div.First().ChildNodes[2].ChildNodes[1].InnerText);

                        }

                        // Karosserie & Fahrwerk
                        HtmlNodeCollection carChassis_div = null;
                        carChassis_div = htmlDoc.DocumentNode.SelectNodes(
                            "//*[@id=\"ctl00_ctl00_cphContentRow_cphContent_wucNFBAutokatalogDetail1_ctl01_updatePanelDetail\"]/div[2]/div[3]/div[4]/table/tbody");

                        if (carChassis_div != null)
                        {
                            carObj.Chassis =
                                HttpUtility.HtmlDecode(carChassis_div.First().ChildNodes[0].ChildNodes[1].InnerText);
                            carObj.Doors =
                                Convert.ToInt32(carChassis_div.First().ChildNodes[1].ChildNodes[1].InnerText);
                            carObj.CarClass =
                                HttpUtility.HtmlDecode(carChassis_div.First().ChildNodes[3].ChildNodes[1].InnerText);
                            carObj.Seats =
                                Convert.ToInt32(carChassis_div.First().ChildNodes[4].ChildNodes[1].InnerText);
                        }

                        //Messwerte Hersteller
                        HtmlNodeCollection carMeasured_div = null;
                        carMeasured_div = htmlDoc.DocumentNode.SelectNodes(
                            "//*[@id=\"ctl00_ctl00_cphContentRow_cphContent_wucNFBAutokatalogDetail1_ctl01_updatePanelDetail\"]/div[2]/div[3]/div[5]/table/tbody");

                        if (carMeasured_div != null)
                        {
                            carObj.SpeedUp =
                                HttpUtility.HtmlDecode(carMeasured_div.First().ChildNodes[0].ChildNodes[1].InnerText);
                            carObj.MaxSpeed =
                                HttpUtility.HtmlDecode(carMeasured_div.First().ChildNodes[1].ChildNodes[1].InnerText);
                            carObj.Tank =
                                HttpUtility.HtmlDecode(carMeasured_div.First().ChildNodes[26].ChildNodes[1].InnerText);
                            carObj.Tank2 =
                                HttpUtility.HtmlDecode(carMeasured_div.First().ChildNodes[27].ChildNodes[1].InnerText);
                        }

                        carDetailsList.Add(carObj);
                    }
                }
                catch (Exception ex)
                {
                    if (this.Core != null && this.Core.Log != null)
                    {
                        this.Core.Log.Error(string.Format("MySQLExporter::CloseConnection : {0} ({1})", ex.Message, modelDetailsUrl));
                    }
                    else
                    {
                        throw new Exception("MySQLExporter::CloseConnection", ex);
                    }
                }
            }
            if (this.Core != null && this.Core.Log != null)
            {
                this.Core.Log.Info(string.Format("{0} Car Detail Records imported.", carDetailsList.Count));
            }

        }
    }
}

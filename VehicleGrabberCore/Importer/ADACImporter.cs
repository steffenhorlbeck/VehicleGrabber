using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using VehicleGrabberCore.DataObjects;

public class ADACImporter : ImporterBase
{
    private string startPath = "/infotestrat/autodatenbank/autokatalog/default.aspx";
    public ADACImporter()
    {
        this.baseUrl = "https://www.adac.de";
        this.baseUrlLang = string.Empty;
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

    public override void StartImport()
    {
        try
        {
            string url = string.Format("{0}{1}{2}", this.baseUrl, this.startPath, this.baseUrlLang);
            this.pageContent = GetContent(url);
            GetMakers();
            GetModels();
            GetCarDetails();
        }
        catch (Exception ex)
        {
            throw new Exception("ADACImporter::ReadPageContent", ex);
        }
    }


    private void GetMakers()
    {
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(this.pageContent);

        HtmlNodeCollection Maker_div = null;
        if (htmlDoc.DocumentNode != null)
        {
            Maker_div = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"ctl00_ctl00_cphContentRow_cphContent_wucNFBAutokatalogMarken1_updatePanelMarken\"]/div[2]/div[2]/ul");

        }

        if (Maker_div != null)
        {
            htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(Maker_div.First().InnerHtml);
            var MakerNodes = htmlDoc.DocumentNode.SelectNodes("//li");

            foreach (var node in MakerNodes)//.Zip(descriptions, (n, d) => new MakerClass { MakerName = n.InnerText, MakerUrlPath = d.InnerText }))
            {
                try
                {
                    MakerObj MakerObj = new MakerObj();

                    MakerObj.MakerName = node.InnerText.Trim();
                    //MakerObj.MakerLogoUrl = node.SelectSingleNode("/li[1]/a[1]/div[1]/img[1]").Attributes //"//*[@id=\"  //
                    //    .AttributesWithName("src").First().Value;

                    MakerObj.MakerLogoUrl = node.ChildNodes[1].ChildNodes[1].FirstChild.ChildAttributes("src").First().Value;
                    //    .AttributesWithName("src").First().Value;

                    //Maker name
                    //   ChildNodes[1].ChildNodes[0].InnerText;

                    string localImgFile = DownloadMakerImage(MakerObj.MakerLogoUrl);
                    MakerObj.MakerLogoLocalFile = localImgFile;

                    //string value = node.Attributes.AttributesWithName("onclick").First().Value;
                    //value = value.Substring(value.IndexOf("'") + 1, value.LastIndexOf("'") - value.IndexOf("'") - 1);
                    //MakerObj.MakerUrlPath = node.SelectSingleNode("/li[1]/a[1]").Attributes.AttributesWithName("href")
                    //    .First().Value;

                    MakerObj.MakerUrlPath = node.ChildNodes[1].Attributes.AttributesWithName("href")
                        .First().Value;

                    this.MakersList.Add(MakerObj);
                    System.Threading.Thread.Sleep(100);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }

    private void GetModels()
    {
        int debug_cnt = 0;
        foreach (MakerObj obj in this.MakersList)
        {
            debug_cnt++;

            string modelsUrl = string.Format("{0}{1}", this.baseUrl, obj.MakerUrlPath);
            string modelsContent = GetContent(modelsUrl);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(modelsContent);

            HtmlNodeCollection model_div = null;
            if (htmlDoc.DocumentNode != null)
            {
                model_div = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"ctl00_ctl00_cphContentRow_cphContent_wucNFBAutokatalogBaureihe1_updatePanelBaureihe\"]/div[2]/div[2]/a");
                //"//*[@id="ctl00_ctl00_cphContentRow_cphContent_wucNFBAutokatalogBaureihe1_updatePanelBaureihe"]/div[2]/div[2]"

            }
            //sleep(1000);

            if (model_div != null)
            {
                foreach (HtmlNode modelNode in model_div)
                {
                    ModelObj modelObj = new ModelObj();

                    try
                    {
                        modelObj.ModelID = this.modelsList.Count + 1;
                        modelObj.ModelName = modelNode.ChildNodes[3].ChildNodes[0].InnerText;
                        modelObj.ModelUrlPath = modelNode.Attributes.AttributesWithName("href").First().Value;
                        modelObj.ModelThumbUrl = modelNode.ChildNodes[1].ChildNodes[1].Attributes
                            .AttributesWithName("src")
                            .First().Value;
                        modelObj.MakerName = obj.MakerName;
                        modelObj.MakerUrlPath = obj.MakerUrlPath;
                        modelObj.MakerLogoUrl = obj.MakerLogoUrl;
                        modelObj.MakerLogoBase64 = obj.MakerLogoBase64;


                        // get Model name

                        // get model link

                        // get image url


                        if (this.modelsList.Find(x =>
                                x.MakerName.ToUpper().Equals(obj.MakerName.ToUpper()) &&
                                x.ModelName.ToUpper().Equals(modelObj.ModelName.ToUpper())) == null)
                        {
                            this.modelsList.Add(modelObj);
                            string localImgFile = DownloadModelImage(modelObj.ModelThumbUrl);
                            modelObj.ModelLocalFile = localImgFile;
                            GetModelTypes(modelObj);
                        }

                        System.Threading.Thread.Sleep(500);
                        
                        //DEBUG: Break after x number of models
                        if (debug_cnt >= 1)
                        {
                            break;
                        }
                        
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            
            
            if (debug_cnt >= 3)
            {
                break;
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
        if (htmlDoc.DocumentNode != null)
        {
            types_div = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"car_db_select_hits\"]/tbody");
        }

        if (types_div != null)
        {
            int debug_cnt = 0;
            foreach (var node in types_div.First().ChildNodes)
            {
                try
                {
                    if (node.Name.ToLower().Equals("tr"))
                    {
                        debug_cnt++;

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
                        typeObj.ModelTypeDoors = int.Parse(node.ChildNodes[9].InnerText.Trim());
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
                        if (debug_cnt >= 3)
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }


    private void GetCarDetails()
    {


        foreach (ModelTypeObj type in modelTypesList)
        {
            string modelDetailsUrl = string.Format("{0}{1}", this.baseUrl, type.ModelTypeDetailsUrl);
            string carContent = GetContent(modelDetailsUrl);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(carContent);

            HtmlNodeCollection cars_div = null;
            if (htmlDoc.DocumentNode != null)
            {
                cars_div = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"ctl00_ctl00_cphContentRow_cphContent_wucNFBAutokatalogDetail1_ctl01_updatePanelDetail\"]/div[2]/div[3]/div[1]/table/tbody");
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
                carObj.InternalClassName =
                    HttpUtility.HtmlDecode(cars_div.First().ChildNodes[5].ChildNodes[1].InnerText);
                carObj.ModelStart = HttpUtility.HtmlDecode(cars_div.First().ChildNodes[6].ChildNodes[1].InnerText);
                carObj.ModelEnd = HttpUtility.HtmlDecode(cars_div.First().ChildNodes[7].ChildNodes[1].InnerText);
                carObj.SeriesStart = HttpUtility.HtmlDecode(cars_div.First().ChildNodes[8].ChildNodes[1].InnerText);
                carObj.SeriesEnd = HttpUtility.HtmlDecode(cars_div.First().ChildNodes[9].ChildNodes[1].InnerText);
                carObj.HSN = HttpUtility.HtmlDecode(cars_div.First().ChildNodes[10].ChildNodes[1].InnerText);
                carObj.TSN = HttpUtility.HtmlDecode(cars_div.First().ChildNodes[11].ChildNodes[1].InnerText);
                carObj.TSN2 = HttpUtility.HtmlDecode(cars_div.First().ChildNodes[12].ChildNodes[1].InnerText);
                carObj.CarTax = HttpUtility.HtmlDecode(cars_div.First().ChildNodes[13].ChildNodes[1].InnerText);
                carObj.CO2Class = HttpUtility.HtmlDecode(cars_div.First().ChildNodes[14].ChildNodes[1].InnerText);
                carObj.BasePrice = HttpUtility.HtmlDecode(cars_div.First().ChildNodes[15].ChildNodes[1].InnerText);
                



                // Motor & Antrieb
                HtmlNodeCollection carEngine_div = null;
                carEngine_div = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"ctl00_ctl00_cphContentRow_cphContent_wucNFBAutokatalogDetail1_ctl01_updatePanelDetail\"]/div[2]/div[3]/div[2]/table/tbody");

                if (carEngine_div != null)
                {
                    carObj.EngineType = HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[0].ChildNodes[1].InnerText);
                    carObj.Fuel = HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[1].ChildNodes[1].InnerText);
                    carObj.Fuel2 = HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[2].ChildNodes[1].InnerText);
                    carObj.EmissionControl = HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[3].ChildNodes[1].InnerText);
                    carObj.EngineDesign = HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[4].ChildNodes[1].InnerText);
                    carObj.Cylinder = int.Parse(carEngine_div.First().ChildNodes[5].ChildNodes[1].InnerText);
                    carObj.FuelType = HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[6].ChildNodes[1].InnerText);
                    carObj.Charge = HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[7].ChildNodes[1].InnerText);
                    carObj.Valves = int.Parse(carEngine_div.First().ChildNodes[8].ChildNodes[1].InnerText);
                    carObj.Cubic = HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[9].ChildNodes[1].InnerText);
                    carObj.PowerKW = int.Parse(carEngine_div.First().ChildNodes[10].ChildNodes[1].InnerText);
                    carObj.PowerPS = int.Parse(carEngine_div.First().ChildNodes[11].ChildNodes[1].InnerText);
                    carObj.MaxPower = HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[12].ChildNodes[1].InnerText);
                    carObj.TurningMoment = HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[13].ChildNodes[1].InnerText);
                    carObj.MaxTurningMoment = HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[14].ChildNodes[1].InnerText);
                    carObj.TypeOfDrive = HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[15].ChildNodes[1].InnerText);
                    carObj.Gearing = HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[16].ChildNodes[1].InnerText);
                    carObj.Gears = int.Parse(carEngine_div.First().ChildNodes[17].ChildNodes[1].InnerText);
                    carObj.StartStopAutomatic = HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[18].ChildNodes[1].InnerText);
                    carObj.EmissionClass = HttpUtility.HtmlDecode(carEngine_div.First().ChildNodes[20].ChildNodes[1].InnerText);
                }


                // Maße & Gewicht
                HtmlNodeCollection carDimensions_div = null;
                carDimensions_div = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"ctl00_ctl00_cphContentRow_cphContent_wucNFBAutokatalogDetail1_ctl01_updatePanelDetail\"]/div[2]/div[3]/div[3]/table/tbody");

                if (carDimensions_div != null)
                {
                    carObj.Length = HttpUtility.HtmlDecode(carDimensions_div.First().ChildNodes[0].ChildNodes[1].InnerText);
                    carObj.Width = HttpUtility.HtmlDecode(carDimensions_div.First().ChildNodes[1].ChildNodes[1].InnerText);
                    carObj.Height = HttpUtility.HtmlDecode(carDimensions_div.First().ChildNodes[2].ChildNodes[1].InnerText);
                    
                }

                // Karosserie & Fahrwerk
                HtmlNodeCollection carChassis_div = null;
                carChassis_div = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"ctl00_ctl00_cphContentRow_cphContent_wucNFBAutokatalogDetail1_ctl01_updatePanelDetail\"]/div[2]/div[3]/div[4]/table/tbody");

                if (carChassis_div != null)
                {
                    carObj.Chassis = HttpUtility.HtmlDecode(carChassis_div.First().ChildNodes[0].ChildNodes[1].InnerText);
                    carObj.Doors = int.Parse(carChassis_div.First().ChildNodes[1].ChildNodes[1].InnerText);
                    carObj.CarClass = HttpUtility.HtmlDecode(carChassis_div.First().ChildNodes[3].ChildNodes[1].InnerText);
                    carObj.Seats = int.Parse(carChassis_div.First().ChildNodes[4].ChildNodes[1].InnerText);
                }

                //Messwerte Hersteller
                HtmlNodeCollection carMeasured_div = null;
                carMeasured_div = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"ctl00_ctl00_cphContentRow_cphContent_wucNFBAutokatalogDetail1_ctl01_updatePanelDetail\"]/div[2]/div[3]/div[5]/table/tbody");

                if (carMeasured_div != null)
                {
                    carObj.SpeedUp = HttpUtility.HtmlDecode(carMeasured_div.First().ChildNodes[0].ChildNodes[1].InnerText);
                    carObj.MaxSpeed = HttpUtility.HtmlDecode(carMeasured_div.First().ChildNodes[1].ChildNodes[1].InnerText);
                    carObj.Tank = HttpUtility.HtmlDecode(carMeasured_div.First().ChildNodes[26].ChildNodes[1].InnerText);
                    carObj.Tank2 = HttpUtility.HtmlDecode(carMeasured_div.First().ChildNodes[27].ChildNodes[1].InnerText);
                }

                carDetailsList.Add(carObj);
            }
        }
    }
}



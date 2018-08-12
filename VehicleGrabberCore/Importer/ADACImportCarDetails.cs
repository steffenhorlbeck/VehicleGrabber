using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using MySql.Data.MySqlClient;
using VehicleGrabberCore.DataObjects;
using VehicleGrabberCore.Exporter;

namespace VehicleGrabberCore.Importer
{
    class ADACImportCarDetails : ImporterBase
    {
        internal MySqlConnection connection;
        internal const string MAKER_TABLE = @"car_maker";
        internal const string MODEL_TABLE = @"car_model";
        internal const string MODELTYPE_TABLE = @"car_modeltype";
        internal const string DETAILS_TABLE = @"car_details";

        private string startPath = "/infotestrat/autodatenbank/autokatalog/default.aspx";
        private string parameters = "ctl00$ctl00$cphContentRow$cphContent$wucNFBAutokatalogMarken1$rb123=radioAllModels";

        private BackgroundWorker bw;
        private string pageContent = string.Empty;


        public ADACImportCarDetails(VGCore core) : base(core)
        {
            this.baseUrl = "https://www.adac.de";
            this.baseUrlLang = string.Empty;

            Initialize();
        }


        //Initialize values
        private void Initialize()
        {

            string user = !string.IsNullOrWhiteSpace(this.Core.Conf.SQLUser)
                ? string.Format("UID={0};", this.Core.Conf.SQLUser)
                : string.Empty;

            string password = !string.IsNullOrWhiteSpace(this.Core.Conf.SQLPassword)
                ? string.Format("PASSWORD={0};", this.Core.Conf.SQLPassword)
                : string.Empty;

            string ssl = !this.Core.Conf.SQLSSLConnection
                ? "SslMode=none;"
                : string.Empty;

            string connectionString = string.Format("SERVER={0};DATABASE={1};{2}{3}{4}", this.Core.Conf.SQLServer, this.Core.Conf.SQLDataBase, user, password, ssl);


            connection = new MySqlConnection(connectionString);

            this.Core.Log.Info(string.Format("SQL Connection set to server '{0}' and database '{1}'", this.Core.Conf.SQLServer, this.Core.Conf.SQLDataBase));
        }



        public override void StartImport(BackgroundWorker bw = null)
        {
            string url = string.Empty;
            try
            {
                SelectModelTypesFromDB();


                GetCarDetails();
                if (bw != null) { bw.ReportProgress(100); }
            }
            catch (Exception ex)
            {
                if (this.Core != null && this.Core.Log != null)
                {
                    this.Core.Log.Error(string.Format("ADACImportCarDetails::ReadPageContent : {0} (URL:{1})", ex.Message, url));
                }
                else
                {
                    throw new Exception("ADACImportCarDetails::ReadPageContent", ex);
                }

            }
        }

        public override string GetPageContent()
        {
            return this.pageContent;
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
                        this.Core.Log.Error(string.Format("MySQLExporter::CloseConnection : {0}", ex.Message));
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





        public void SelectModelTypesFromDB()
        {
            string query = string.Format("SELECT * FROM {0}", MySQLExporter.MODELTYPE_TABLE);

            /*
            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            */



            modelTypesList.Clear();
            modelTypesList.Capacity = 1;


            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    ModelTypeObj obj = new ModelTypeObj();


                    obj.MakerID = Convert.ToInt32(dataReader["maker_id"] + "");
                    obj.ModelID = Convert.ToInt32(dataReader["model_id"] + "");
                    obj.ModelTypeID = Convert.ToInt32(dataReader["modeltype_id"] + "");
                    obj.ModelTypeName = dataReader["name"] + "";
                    obj.ModelTypeDetailsUrl = dataReader["type_url"] + "";

                    modelTypesList.Capacity++;
                    modelTypesList.Add(obj);
                    /*
                    list[0].Add(dataReader["id"] + "");
                    list[1].Add(dataReader["name"] + "");
                    list[2].Add(dataReader["age"] + "");
                    */

                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

            }
        }



        //open connection to database
        internal bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                if (this.Core != null && this.Core.Log != null)
                {
                    this.Core.Log.Error(string.Format("MySQLExporter::OpenConnection : {0}", ex.Message));
                }
                else
                {
                    throw new Exception("MySQLExporter::OpenConnection", ex);
                }
                return false;
            }
        }

        //Close connection
        internal bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                if (this.Core != null && this.Core.Log != null)
                {
                    this.Core.Log.Error(string.Format("MySQLExporter::CloseConnection : {0}", ex.Message));
                }
                else
                {
                    throw new Exception("MySQLExporter::CloseConnection", ex);
                }
                return false;
            }
        }

        //Count statement
        internal int GetModelTypeCount()
        {
            string query = "SELECT Count(*) FROM tableinfo";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = Convert.ToInt32(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }
    }
}

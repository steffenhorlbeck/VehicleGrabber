﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;
using VehicleGrabberCore.DataObjects;

namespace VehicleGrabberCore.Exporter
{
    

    class MySQLExporter
    {
        internal MySqlConnection connection;
        internal const string MAKER_TABLE = @"car_maker";
        internal const string MODEL_TABLE = @"car_model";
        internal const string MODELTYPE_TABLE = @"car_modeltype";
        internal const string DETAILS_TABLE = @"car_details";

        public string SqlServer { get; } = string.Empty;
        public string SqlDatabase { get; } = string.Empty;
        public bool Sqlssl { get; } = true;
        public long SqlPort { get; } = 3306;
        public string SqlUser { get; } = string.Empty;
        public string SqlPassword { get; } = string.Empty;

        public List<MakerObj> MakersObjList;
        public List<ModelObj> modelObjList;
        public List<ModelTypeObj> modelTypeObjList;
        public List<CarDetailsObj> carDetailsObjList;
        private readonly SQLCarMaker _sqlCarMaker;
        private readonly SQLCarModel _sqlCarModel;
        private readonly SQLCarModelType _sqlCarModelType;
        private readonly SQLCarDetails _sqlCarDetail;

        public VGCore Core { get; set; }

        public MySQLExporter(VGCore core, List<MakerObj> MakerObjObjects, List<ModelObj> modelsObjects,
            List<ModelTypeObj> modelTypesObjects, List<CarDetailsObj> carDetailsObjects, string server = "megraso.de",
            string database = "h26346_cardata", bool ssl = true,
            long port = 3306, string user = "h26346_cardata", string password = "1Master!01")
        {
            this.SqlServer = server;
            this.SqlDatabase = database;
            this.Sqlssl = ssl;
            this.SqlPort = port;
            this.SqlUser = user;
            this.SqlPassword = password;

            this.MakersObjList = MakerObjObjects;
            this.modelObjList = modelsObjects;
            this.modelTypeObjList = modelTypesObjects;
            this.carDetailsObjList = carDetailsObjects;

            this.Core = core;

            Initialize();
            _sqlCarMaker = new SQLCarMaker(this);

            _sqlCarModel = new SQLCarModel(this);

            _sqlCarModelType = new SQLCarModelType(this);

            _sqlCarDetail = new SQLCarDetails(this);

        }

        //Initialize values
        private void Initialize()
        {

            string user = !string.IsNullOrWhiteSpace(this.SqlUser)
                ? string.Format("UID={0};", this.SqlUser)
                : string.Empty;
            
            string password = !string.IsNullOrWhiteSpace(this.SqlPassword)
                ? string.Format("PASSWORD={0};", this.SqlPassword)
                : string.Empty;

            string ssl = !this.Sqlssl
                ? "SslMode=none;"
                : string.Empty;

            string connectionString = string.Format("SERVER={0};DATABASE={1};{2}{3}{4}", this.SqlServer, this.SqlDatabase, user, password, ssl);

            
            connection = new MySqlConnection(connectionString);

            this.Core.Log.Info(string.Format("SQL Connection set to server '{0}' and database '{1}'", this.SqlServer, this.SqlDatabase));
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
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.

                //switch (ex.Number)
                //{
                //    case 0:
                        if (this.Core != null && this.Core.Log != null)
                        {
                            this.Core.Log.Error(string.Format("MySQLExporter::OpenConnection : {0}", ex.Message));
                        }
                        else
                        {
                            throw new Exception("MySQLExporter::OpenConnection", ex);
                        }

                /*    case 1045:
                        if (this.Core != null && this.Core.Log != null)
                        {
                            this.Core.Log.Error(string.Format("MySQLExporter::OpenConnection : {0}", ex.Message));
                        }
                        else
                        {
                            throw new Exception("MySQLExporter::OpenConnection", ex); //wrong username or password
                        }
                }*/
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

        public void HandleMakers()
        {            
            _sqlCarMaker.Add_CarMakers();            
            this.Core.Log.Info(string.Format("{0} makers exported to database", this.MakersObjList.Count));
        }

        public void HandleModels()
        {
            _sqlCarModel.Add_CarModels();
            this.Core.Log.Info(string.Format("{0} models exported to database", this.modelObjList.Count));
        }

        public void HandleModelTypes()
        {
            _sqlCarModelType.Add_CarModelTypes();
            this.Core.Log.Info(string.Format("{0} model types exported to database", this.modelTypeObjList.Count));
        }

        public void HandleCarDetails()
        {
            _sqlCarDetail.Add_CarDetails();
            this.Core.Log.Info(string.Format("{0} cars exported to database", this.carDetailsObjList.Count));
        }

        public long GetMakerID(string name)
        {
            long id = SQLCarMaker.GetMakerId(name);
            return id;
        }

        // ************************************************************************************************************


        // ************************************************************************************************************

        //Delete statement
        public void Delete()
        {
            string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        //Select statement
        public List<string>[] Select()
        {
            string query = "SELECT * FROM tableinfo";

            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();

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
                    list[0].Add(dataReader["id"] + "");
                    list[1].Add(dataReader["name"] + "");
                    list[2].Add(dataReader["age"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        //Count statement
        public int Count()
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

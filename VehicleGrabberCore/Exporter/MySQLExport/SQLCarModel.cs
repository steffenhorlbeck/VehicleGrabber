using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using VehicleGrabberCore.DataObjects;

namespace VehicleGrabberCore.Exporter
{
    class SQLCarModel
    {
        private static MySQLExporter _mySqlExporter;

        private static VGCore Core { get; set; }

        public SQLCarModel(MySQLExporter mySqlExporter)
        {
            _mySqlExporter = mySqlExporter;
            Core = mySqlExporter.Core;
        }

        public void Add_CarModels()
        {
            foreach (ModelObj obj in _mySqlExporter.modelObjList)
            {
                try
                {
                    long makerId = SQLCarMaker.GetMakerId(obj.MakerName);
                    long modelId = GetModelId(obj.ModelName);
                    if (modelId == -1)
                    {
                        Insert_CarModel(obj, makerId);
                    }
                    else
                    {                        
                        Update_CarModel(obj, makerId, modelId);
                    }                    
                }
                catch (Exception ex)
                {
                    if (Core != null && Core.Log != null)
                    {
                        Core.Log.Error(string.Format("SQLCarModel::Add_CarModels : {0}", ex.Message));
                    }
                    else
                    {
                        throw new Exception("SQLCarModel::Add_CarModels", ex);
                    }
                    
                }
            }
        }



        public void Insert_CarModel(ModelObj obj, long makerId)
        {
            try
            {
                string query = string.Format("INSERT INTO {0} " +
                                             "(name, maker, maker_id, image, model_url, img_url) " +
                                             "VALUES" +
                                             "(@name, @maker, @maker_id, @image, @model_url, @img_url)",
                    MySQLExporter.MODEL_TABLE);


                System.IO.FileStream fs = new FileStream(obj.ModelLocalFile, FileMode.Open);
                System.IO.BufferedStream bf = new BufferedStream(fs);
                byte[] buffer = new byte[bf.Length];
                bf.Read(buffer, 0, buffer.Length);

                byte[] buffer_new = buffer;

                //open connection
                if (_mySqlExporter.connection.State == ConnectionState.Open || _mySqlExporter.OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, _mySqlExporter.connection);

                    cmd.CommandText = query;

                    SetSQLParameters(obj, cmd, makerId, -1, buffer_new);

                    //Execute command
                    cmd.ExecuteNonQuery();

                    //close connection
                    _mySqlExporter.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                if (Core != null && Core.Log != null)
                {
                    Core.Log.Error(string.Format("SQLCarModel::Insert_CarModel : {0}", ex.Message));
                }
                else
                {
                    throw new Exception("SQLCarModel::Insert_CarModel", ex);
                }
            }
        }



        public void Update_CarModel(ModelObj obj, long makerId, long modelId)
        {
            try
            {
                string query = string.Format("UPDATE {0} SET " +
                                             "name=@name, " +
                                             "maker=@maker, " +
                                             "maker_id=@maker_id, " +
                                             "image=@image, " +
                                             "model_url=@model_url, " +
                                             "img_url=@img_url " +
                                             "WHERE id={1}", MySQLExporter.MODEL_TABLE,
                    modelId);

                System.IO.FileStream fs = new FileStream(obj.ModelLocalFile, FileMode.Open);
                System.IO.BufferedStream bf = new BufferedStream(fs);
                byte[] buffer = new byte[bf.Length];
                bf.Read(buffer, 0, buffer.Length);

                byte[] buffer_new = buffer;

                //Open connection
                if (_mySqlExporter.connection.State == ConnectionState.Open || _mySqlExporter.OpenConnection() == true)
                {
                    //create mysql command
                    MySqlCommand cmd = new MySqlCommand
                    {
                        CommandText = query,
                        Connection = _mySqlExporter.connection
                    };
                    //Assign the query using CommandText
                    //Assign the connection using Connection

                    SetSQLParameters(obj, cmd, makerId, modelId, buffer_new);

                    //Execute query
                    cmd.ExecuteNonQuery();

                    //close connection
                    _mySqlExporter.CloseConnection();
                }
            }
            catch (Exception ex)
            {                
                if (Core != null && Core.Log != null)
                {
                    Core.Log.Error(string.Format("SQLCarModel::Update_CarModel : {0}", ex.Message));
                }
                else
                {
                    throw new Exception("SQLCarModel::Update_CarModel", ex);
                }
            }
        }

        public static long GetModelId(string model)
        {
            long id = -1;
            try
            {
                string query = string.Format("SELECT id FROM {0} WHERE name = upper('{1}')", MySQLExporter.MODEL_TABLE, model.ToUpper());

                //Open Connection
                if (_mySqlExporter.connection.State == ConnectionState.Open || _mySqlExporter.OpenConnection() == true)
                {
                    //Create Mysql Command
                    MySqlCommand cmd = new MySqlCommand(query, _mySqlExporter.connection);

                    //cmd.CommandText = query;

                    //ExecuteScalar will return one value
                    var retVal = cmd.ExecuteScalar();
                    id = retVal == null ? -1 : Convert.ToInt32(retVal);

                    //close Connection
                    _mySqlExporter.CloseConnection();

                }

                return id;
            }
            catch (Exception ex)
            {
                if (Core != null && Core.Log != null)
                {
                    Core.Log.Error(string.Format("SQLCarModel::Add_CarModels : {0}", ex.Message));
                }
                else
                {
                    throw new Exception("SQLCarModel::Add_CarModels", ex);
                }

                return id;
            }
        }

        private static void SetSQLParameters(ModelObj obj, MySqlCommand cmd, long makerId, long modelId, byte[] buffer_new)
        {
            cmd.Parameters.AddWithValue("@name", obj.ModelName);
            cmd.Parameters.AddWithValue("@maker", obj.MakerName);
            cmd.Parameters.AddWithValue("@maker_id", makerId);
            cmd.Parameters.AddWithValue("@image", buffer_new);
            cmd.Parameters.AddWithValue("@model_url", obj.ModelUrlPath);
            cmd.Parameters.AddWithValue("@img_url", obj.ModelThumbUrl);
        }

        private bool ModelExists(string model)
        {
            bool result = false;
            try
            {
                string query = string.Format("SELECT Count(*) FROM {0} WHERE name LIKE '{1}'", MySQLExporter.MODEL_TABLE, model);
                int Count = -1;

                //Open Connection
                if (_mySqlExporter.connection.State == ConnectionState.Open || _mySqlExporter.OpenConnection() == true)
                {
                    //Create Mysql Command
                    MySqlCommand cmd = new MySqlCommand(query, _mySqlExporter.connection);

                    cmd.CommandText = query;

                    //ExecuteScalar will return one value
                    Count = Convert.ToInt32(cmd.ExecuteScalar() + "");

                    //close Connection
                    _mySqlExporter.CloseConnection();

                    if (Count > 0)
                    {
                        result = true;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                if (Core != null && Core.Log != null)
                {
                    Core.Log.Error(string.Format("SQLCarModel::ModelExists : {0}", ex.Message));
                }
                else
                {
                    throw new Exception("SQLCarModel::ModelExists", ex);
                }

                return result;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using VehicleGrabberCore.DataObjects;

namespace VehicleGrabberCore.Exporter
{
    class SQLCarModelType
    {
        private static MySQLExporter _mySqlExporter;

        private static VGCore Core { get; set; }

        public SQLCarModelType(MySQLExporter mySqlExporter)
        {
            _mySqlExporter = mySqlExporter;
            Core = mySqlExporter.Core;
        }

        public void Add_CarModelTypes()
        {            
            foreach (ModelTypeObj obj in _mySqlExporter.modelTypeObjList)
            {
                try
                {
                   
                    long makerId = SQLCarMaker.GetMakerId(obj.MakerName);
                    long modelId = SQLCarModel.GetModelId(obj.ModelName);
                    if (!ModelTypeExists(obj.ModelTypeName))
                    {
                        Insert_CarModel(obj, makerId, modelId);
                    }
                    else
                    {
                        long id = GetModelTypeId(obj.ModelTypeName);
                        Update_CarModel(obj, makerId, modelId, id);
                    }

                }
                catch (Exception ex)
                {
                    if (Core != null && Core.Log != null)
                    {
                        Core.Log.Error(string.Format("SQLCarModelType::Add_CarModelTypes : {0}", ex.Message));
                    }
                    else
                    {
                        throw new Exception("SQLCarModelType::Add_CarModelTypes", ex);
                    }
                    
                }
            }
        }

        public void Insert_CarModel(ModelTypeObj obj, long makerId, long modelId)
        {
            string query = string.Format("INSERT INTO {0} " +
                                         "(maker_id, model_id, modeltype_id, name, cubic, fuel, power, tank, from_year, to_year, chassis, doors, type_url) " +
                                         "VALUES" +
                                         "(@maker_id, @model_id, @modeltype_id, @name, @cubic, @fuel, @power, @tank, @from_year, @to_year, @chassis, @doors, @type_url)", MySQLExporter.MODELTYPE_TABLE);


            //open connection
            if (_mySqlExporter.connection.State == ConnectionState.Open || _mySqlExporter.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, _mySqlExporter.connection);


                SetSQLParameters(obj, cmd, makerId, modelId);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                _mySqlExporter.CloseConnection();
            }
        }

        public void Update_CarModel(ModelTypeObj obj, long makerId, long modelId, long id)
        {
            string query = string.Format("UPDATE {0} SET " +
                                         "maker_id=@maker_id, " +
                                         "model_id=@model_id, " +
                                         "modeltype_id=@modeltype_id" +
                                         "name=@name" +
                                         "cubic=@cubic" +
                                         "fuel=@fuel" +
                                         "power=@power" +
                                         "tank=@tank" +
                                         "from_year=@from_year" +
                                         "to_year=@to_year" +
                                         "chassis=@chassis" +
                                         "doors=@doors" +
                                         "type_url=@type_url" +
                                         " WHERE id={1}", MySQLExporter.MODELTYPE_TABLE,
                id);


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

                SetSQLParameters(obj, cmd, makerId, modelId);

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                _mySqlExporter.CloseConnection();
            }
        }


        public static long GetModelTypeId(string modeltype)
        {
            long id = -1;
            try
            {
                string query = string.Format("SELECT id FROM {0} WHERE name LIKE '{1}'", MySQLExporter.MODELTYPE_TABLE, modeltype);
                //int Count = -1;

                //Open Connection
                if (_mySqlExporter.connection.State == ConnectionState.Open || _mySqlExporter.OpenConnection() == true)
                {
                    //Create Mysql Command
                    MySqlCommand cmd = new MySqlCommand(query, _mySqlExporter.connection);

                    //ExecuteScalar will return one value
                    var retVal = cmd.ExecuteScalar() + "";
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
                    Core.Log.Error(string.Format("SQLCarModelType::ModelTypeExists : {0}", ex.Message));
                }
                else
                {
                    throw new Exception("SQLCarModelType::ModelTypeExists", ex);
                }

                return id;
            }
        }


        private static void SetSQLParameters(ModelTypeObj obj, MySqlCommand cmd, long makerId, long modelId)
        {
            cmd.Parameters.AddWithValue("@maker_id", makerId);
            cmd.Parameters.AddWithValue("@model_id", modelId);
            //cmd.Parameters.AddWithValue("@modeltype_id", obj.ModelTypeID);
            cmd.Parameters.AddWithValue("@name", obj.ModelTypeName);
            cmd.Parameters.AddWithValue("@cubic", obj.ModelTypeCubic);
            cmd.Parameters.AddWithValue("@fuel", obj.ModelTypeFuel);
            cmd.Parameters.AddWithValue("@power", obj.ModelTypePower);
            cmd.Parameters.AddWithValue("@tank", obj.ModelTypeTank);
            cmd.Parameters.AddWithValue("@from_year", obj.ModelTypeFromYear);
            cmd.Parameters.AddWithValue("@to_year", obj.ModelTypeToYear);
            cmd.Parameters.AddWithValue("@chassis", obj.ModelTypeChassis);
            cmd.Parameters.AddWithValue("@doors", obj.ModelTypeDoors);
            cmd.Parameters.AddWithValue("@type_url", obj.ModelTypeDetailsUrl);
        }

        private bool ModelTypeExists(string modeltype)
        {
            bool result = false;
            try
            {
                string query = string.Format("SELECT Count(*) FROM {0} WHERE name LIKE '{1}'", MySQLExporter.MODELTYPE_TABLE, modeltype);
                int Count = -1;

                //Open Connection
                if (_mySqlExporter.connection.State == ConnectionState.Open || _mySqlExporter.OpenConnection() == true)
                {
                    //Create Mysql Command
                    MySqlCommand cmd = new MySqlCommand(query, _mySqlExporter.connection);

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
                    Core.Log.Error(string.Format("SQLCarModelType::ModelTypeExists : {0}", ex.Message));
                }
                else
                {
                    throw new Exception("SQLCarModelType::ModelTypeExists", ex);
                }

                return result;
            }
        }
    }
}

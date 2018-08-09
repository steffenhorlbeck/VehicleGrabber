using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;
using VehicleGrabberCore.DataObjects;
using VehicleGrabberCore.Exporter;

namespace VehicleGrabberCore.Exporter
{    
    class SQLCarMaker
    {
        private static MySQLExporter _mySqlExporter;

        private static VGCore Core { get; set; }

        public SQLCarMaker(MySQLExporter mySqlExporter)
        {
            _mySqlExporter = mySqlExporter;
            Core = mySqlExporter.Core;
        }

        public void Add_CarMakers()
        {
            foreach (MakerObj maker in _mySqlExporter.MakersObjList)
            {
                try
                {
                    //if (!MakerExists(maker.MakerName))
                    long id = GetMakerId(maker.MakerName);
                    if(id == -1)
                    {
                        Insert_CarMaker(maker);
                    }
                    else
                    {
                        Update_CarMaker(id, maker);
                    }

                }
                catch (Exception ex)
                {
                    if (Core != null && Core.Log != null)
                    {
                        Core.Log.Error(string.Format("SQLCarMaker::Add_CarMakers : {0}", ex.Message));
                    }
                    else
                    {
                        //throw new Exception("SQLCarMaker::Add_CarMakers", ex);
                    }
                    
                }
            }
        }

        public void Insert_CarMaker(MakerObj maker)
        {
            string query = string.Format("INSERT INTO {0} (name, url, logo) VALUES (@name, @url, @logo)",
                MySQLExporter.MAKER_TABLE);


            System.IO.FileStream fs = new FileStream(maker.MakerLogoLocalFile, FileMode.Open);
            System.IO.BufferedStream bf = new BufferedStream(fs);
            byte[] buffer = new byte[bf.Length];
            bf.Read(buffer, 0, buffer.Length);

            byte[] buffer_new = buffer;

            //open connection
            if (_mySqlExporter.connection.State == ConnectionState.Open || _mySqlExporter.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, _mySqlExporter.connection);

                cmd.Parameters.AddWithValue("@name", maker.MakerName);
                cmd.Parameters.AddWithValue("@url", string.Empty);
                cmd.Parameters.AddWithValue("@logo", buffer_new);


                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                _mySqlExporter.CloseConnection();
            }
        }

        public void Update_CarMaker(long id, MakerObj maker)
        {
            string query = string.Format("UPDATE {0} SET name=@name, url=@url, logo=@logo WHERE id = {1};",
                MySQLExporter.MAKER_TABLE,
                id);

            System.IO.FileStream fs = new FileStream(maker.MakerLogoLocalFile, FileMode.Open);
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

                cmd.Parameters.AddWithValue("@name", maker.MakerName);
                cmd.Parameters.AddWithValue("@url", string.Empty);
                cmd.Parameters.AddWithValue("@logo", buffer_new);

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                _mySqlExporter.CloseConnection();
            }
        }

        public static long GetMakerId(string name)
        {
            long id = -1;
            try
            {
                string query = string.Format("SELECT id FROM {0} WHERE upper(name) LIKE '{1}'", MySQLExporter.MAKER_TABLE, name.ToUpper());
                

                //Open Connection
                if (_mySqlExporter.connection.State == ConnectionState.Open || _mySqlExporter.OpenConnection() == true)
                {
                    //Create Mysql Command
                    MySqlCommand cmd = new MySqlCommand(query, _mySqlExporter.connection);

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
                    Core.Log.Error(string.Format("SQLCarMaker::GetMakerId : {0}", ex.Message));
                }
                else
                {
                    throw new Exception("SQLCarMaker::GetMakerId", ex);
                }
                return id;
            }

        }

        private bool MakerExists(string maker)
        {
            bool result = false;
            try
            {
                string query = string.Format("SELECT Count(*) FROM {0} WHERE upper(name) LIKE '{1}'", MySQLExporter.MAKER_TABLE,
                    maker.ToUpper());
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
                    Core.Log.Error(string.Format("SQLCarMaker::MakerExists : {0}", ex.Message));
                }
                else
                {
                    throw new Exception("SQLCarMaker::MakerExists", ex);
                }

                return result;
            }
        }
    }
}
using System;
using System.Collections.Generic;
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

        public SQLCarMaker(MySQLExporter mySqlExporter)
        {
            _mySqlExporter = mySqlExporter;
        }

        public void Add_CarMakers()
        {
            foreach (MakerObj maker in _mySqlExporter.MakersObjList)
            {
                try
                {
                    if (!MakerExists(maker.MakerName))
                    {
                        Insert_CarMaker(maker);
                    }
                    else
                    {
                        Update_CarMaker(maker);
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("SQLCarMaker::Add_CarMakers", ex);
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
            if (_mySqlExporter.OpenConnection() == true)
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

        public void Update_CarMaker(MakerObj maker)
        {
            string query = string.Format("UPDATE {0} SET name=@name, url=@url, logo=@logo WHERE name LIKE '{1}'",
                MySQLExporter.MAKER_TABLE,
                maker.MakerName);

            System.IO.FileStream fs = new FileStream(maker.MakerLogoLocalFile, FileMode.Open);
            System.IO.BufferedStream bf = new BufferedStream(fs);
            byte[] buffer = new byte[bf.Length];
            bf.Read(buffer, 0, buffer.Length);

            byte[] buffer_new = buffer;

            //Open connection
            if (_mySqlExporter.OpenConnection() == true)
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

        public static int GetMakerId(string name)
        {
            try
            {
                string query = string.Format("SELECT id FROM {0} WHERE name LIKE {1}", MySQLExporter.MAKER_TABLE, name);
                int id = -1;

                //Open Connection
                if (_mySqlExporter.OpenConnection() == true)
                {
                    //Create Mysql Command
                    MySqlCommand cmd = new MySqlCommand(query, _mySqlExporter.connection);

                    //ExecuteScalar will return one value
                    id = int.Parse(cmd.ExecuteScalar() + "");

                    //close Connection
                    _mySqlExporter.CloseConnection();
                }

                return id;
            }
            catch (Exception ex)
            {
                throw new Exception("SQLCarMaker::GetMakerId", ex);
            }

        }

        private bool MakerExists(string maker)
        {
            bool result = false;
            try
            {
                string query = string.Format("SELECT Count(*) FROM {0} WHERE name LIKE {1}", MySQLExporter.MAKER_TABLE,
                    maker);
                int Count = -1;

                //Open Connection
                if (_mySqlExporter.OpenConnection() == true)
                {
                    //Create Mysql Command
                    MySqlCommand cmd = new MySqlCommand(query, _mySqlExporter.connection);

                    //ExecuteScalar will return one value
                    Count = int.Parse(cmd.ExecuteScalar() + "");

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
                throw new Exception("SQLCarMaker::MakerExists", ex);
            }
        }
    }
}
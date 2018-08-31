using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using VehicleGrabberCore.DataObjects;
using VehicleGrabberCore.Exporter;

namespace VehicleGrabberCore.Exporter
{
    class SQLCarDetails
    {
        private MySQLExporter _mySqlExporter;

        private VGCore Core { get; set; }

        public SQLCarDetails(MySQLExporter mySqlExporter)
        {
            _mySqlExporter = mySqlExporter;
            Core = mySqlExporter.Core;
        }

        public void Add_CarDetails()
        {
            foreach (CarDetailsObj obj in _mySqlExporter.carDetailsObjList)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(obj.Maker))
                    {
                        obj.Maker = Core.Conf.MakerName;
                    }
                    long makerId = SQLCarMaker.GetMakerId(obj.Maker);
                    long modelId = SQLCarModel.GetModelId(obj.Series);
                    long typeId = SQLCarModelType.GetModelTypeId(obj.ModelTypeName);

                    //long id = GetCarDetailsId(makerId, modelId, typeId, obj.ModelTypeName);
                    long id = GetCarDetailsId(obj.HSN, obj.TSN, obj.ModelTypeName);
                    if (id == -1)
                    {
                        Insert_CarDetails(obj, makerId, modelId, typeId);
                    }
                    else
                    {

                        Update_CarDetails(obj, makerId, modelId, typeId, id);
                    }

                }
                catch (Exception ex)
                {
                    if (Core != null && Core.Log != null)
                    {
                        Core.Log.Error(string.Format("SQLCarDetails::Add_CarDetails : {0}", ex.Message));
                    }
                    else
                    {
                        throw new Exception("SQLCarDetails::Add_CarDetails", ex);
                    }
                    
                }
            }
        }

        public void Insert_CarDetails(CarDetailsObj obj, long makerId, long modelId, long typeId)
        {
            try
            {
                string query = string.Format("INSERT INTO {0} " +
                                             "(maker_id, model_id, modeltype_id, maker, model, type, series, modeltypename, internal_class_name, model_start, model_end, " +
                                             "series_start, series_end, hsn, tsn, tsn2, car_tax, co2_class, base_price, engine_type, fuel, fuel2, " +
                                             "emission_control, engine_design, cylinder, fuel_type, charge, valves, cubic, power_kw, power_ps, " +
                                             "max_power, turning_moment, max_turning_moment, type_of_drive, gearing, gears, start_stop_automatic, " +
                                             "emission_class, length, width, height, chassis, doors, car_class, seats, speed_up, max_speed, tank, tank2) " +
                                             "VALUES" +
                                             "(@maker_id, @model_id, @modeltype_id, @maker, @model, @type, @series, @modeltypename, @internal_class_name, STR_TO_DATE(@model_start, '%d/%m/%Y'), STR_TO_DATE(@model_end, '%d/%m/%Y'), " +
                                             "STR_TO_DATE(@series_start, '%d/%m/%Y'), STR_TO_DATE(@series_end, '%d/%m/%Y'), @hsn, @tsn, @tsn2, @car_tax, @co2_class, @base_price, @engine_type, @fuel, @fuel2, " +
                                             "@emission_control, @engine_design, @cylinder, @fuel_type, @charge, @valves, @cubic, @power_kw, @power_ps, " +
                                             "@max_power, @turning_moment, @max_turning_moment, @type_of_drive, @gearing, @gears, @start_stop_automatic, " +
                                             "@emission_class, @length, @width, @height, @chassis, @doors, @car_class, @seats, @speed_up, @max_speed, @tank, @tank2)",
                    MySQLExporter.DETAILS_TABLE);


                //open connection
                if (_mySqlExporter.connection.State == ConnectionState.Open || _mySqlExporter.OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, _mySqlExporter.connection);

                    //cmd.CommandText = query;

                    SetSQLParameters(obj, cmd, makerId, modelId, typeId);

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
                    Core.Log.Error(string.Format("SQLCarDetails::Insert_CarDetails : {0}", ex.Message));
                }
                else
                {
                    throw new Exception("SQLCarDetails::Insert_CarDetails", ex);
                }
            }
        }

        public void Update_CarDetails(CarDetailsObj obj, long makerId, long modelId, long typeId, long id)
        {
            try
            {
                string query = string.Format("UPDATE {0} SET " +
                                             "maker_id=@maker_id, " +
                                             "model_id=@model_id, " +
                                             "modeltype_id=@modeltype_id, " +
                                             "maker=@maker, " +
                                             "model=@model, " +
                                             "type=@type, " +
                                             "series=@series, " +
                                             "modeltypename=@modeltypename, " +
                                             "internal_class_name=@internal_class_name, " +
                                             "model_start=STR_TO_DATE(@model_start, '%d/%m/%Y'), " +
                                             "model_end=STR_TO_DATE( @model_end, '%d/%m/%Y'), " +
                                             "series_start=STR_TO_DATE(@series_start, '%d/%m/%Y') , " +
                                             "series_end=STR_TO_DATE(@series_end, '%d/%m/%Y') , " +
                                             "hsn=@hsn, " +
                                             "tsn=@tsn, " +
                                             "tsn2=@tsn2, " +
                                             "car_tax=@car_tax, " +
                                             "co2_class=@co2_class, " +
                                             "base_price=@base_price, " +
                                             "engine_type=@engine_type, " +
                                             "fuel=@fuel, " +
                                             "fuel2=@fuel2, " +
                                             "emission_control=@emission_control, " +
                                             "engine_design=@engine_design, " +
                                             "cylinder=@cylinder, " +
                                             "fuel_type=@fuel_type, " +
                                             "charge=@charge, " +
                                             "valves=@valves, " +
                                             "cubic=@cubic, " +
                                             "power_kw=@power_kw, " +
                                             "power_ps=@power_ps, " +
                                             "max_power=@max_power, " +
                                             "turning_moment=@turning_moment, " +
                                             "max_turning_moment=@max_turning_moment, " +
                                             "type_of_drive=@type_of_drive, " +
                                             "gearing=@gearing, " +
                                             "gears=@gears, " +
                                             "start_stop_automatic=@start_stop_automatic, " +
                                             "emission_class=@emission_class, " +
                                             "length=@length, " +
                                             "width=@width, " +
                                             "height=@height, " +
                                             "chassis=@chassis, " +
                                             "doors=@doors, " +
                                             "car_class=@car_class, " +
                                             "seats=@seats, " +
                                             "speed_up=@speed_up, " +
                                             "max_speed=@max_speed, " +
                                             "tank=@tank, " +
                                             "tank2=@tank2" +
                                             " WHERE id={1}", MySQLExporter.DETAILS_TABLE,
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

                    SetSQLParameters(obj, cmd, makerId, modelId, typeId);


                    //Execute query
                    cmd.ExecuteNonQuery();

                    //close connection
                    _mySqlExporter.CloseConnection();
                }
            }catch(Exception ex)
            {
                if (Core != null && Core.Log != null)
                {
                    Core.Log.Error(string.Format("SQLCarDetails::Update_CarDetails : {0}", ex.Message));
                }
                else
                {
                    throw new Exception("SQLCarDetails::Update_CarDetails", ex);
                }
            }
        }


        public long GetCarDetailsId(long maker_id, long model_id, long modeltype_id, string modeltypename)
        {
            long id = -1;
            try
            {
                string query = string.Format(
                    "SELECT id FROM {0} WHERE maker_id = {1} AND model_id = {2} AND modeltype_id = {3} AND modeltypename = upper({4});",
                    MySQLExporter.DETAILS_TABLE,
                    maker_id, model_id, modeltype_id, modeltypename.ToUpper());

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
                    Core.Log.Error(string.Format("SQLCarDetails::GetCarDetailsId : {0}", ex.Message));
                }
                else
                {
                    throw new Exception("SQLCarDetails::GetCarDetailsId", ex);
                }

                return id;
            }
        }


        public long GetCarDetailsId(string hsn, string tsn, string modeltypename)
        {
            long id = -1;
            try
            {
                string query = string.Format(
                    "SELECT id FROM {0} WHERE hsn = '{1}' AND tsn = '{2}' AND modeltypename = upper({3});",
                    MySQLExporter.DETAILS_TABLE,
                    hsn, tsn, modeltypename.ToUpper());

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
                    Core.Log.Error(string.Format("SQLCarDetails::GetCarDetailsId : {0}", ex.Message));
                }
                else
                {
                    throw new Exception("SQLCarDetails::GetCarDetailsId", ex);
                }

                return id;
            }
        }



        private static void SetSQLParameters(CarDetailsObj obj, MySqlCommand cmd, long makerId, long modelId,
            long typeId)
        {
            cmd.Parameters.AddWithValue("@maker_id", makerId);
            cmd.Parameters.AddWithValue("@model_id", modelId);
            cmd.Parameters.AddWithValue("@modeltype_id", typeId);
            cmd.Parameters.AddWithValue("@maker", obj.Maker);
            cmd.Parameters.AddWithValue("@model", obj.Model);
            cmd.Parameters.AddWithValue("@type", obj.Type);
            cmd.Parameters.AddWithValue("@series", obj.Series);
            cmd.Parameters.AddWithValue("@modeltypename", obj.ModelTypeName);
            cmd.Parameters.AddWithValue("@internal_class_name", obj.InternalClassName);

            //dateTime ?
            cmd.Parameters.AddWithValue("@model_start", obj.ModelStart.Length <= 7 ? string.Format("01/{0}",obj.ModelStart) : obj.ModelStart);

            if (string.IsNullOrWhiteSpace(obj.ModelEnd))
            {
                cmd.Parameters.AddWithValue("@model_end", obj.ModelEnd);
            }
            else
            {
                string[] parts = obj.ModelEnd.Split('/');
                string lastday = DateTime.DaysInMonth(Convert.ToInt32(parts[1]), Convert.ToInt32(parts[0])).ToString();
                cmd.Parameters.AddWithValue("@model_end",
                    obj.ModelEnd.Length <= 7 ? string.Format("{0}/{1}", lastday, obj.ModelEnd) : obj.ModelEnd);
            }

            cmd.Parameters.AddWithValue("@series_start", obj.SeriesStart.Length <= 7 ? string.Format("01/{0}", obj.SeriesStart) : obj.SeriesStart);

            if (string.IsNullOrWhiteSpace(obj.SeriesEnd))
            {
                cmd.Parameters.AddWithValue("@series_end", obj.SeriesEnd);
            }
            else
            {
                string[] parts = obj.SeriesEnd.Split('/');
                string lastday = DateTime.DaysInMonth(Convert.ToInt32(parts[1]), Convert.ToInt32(parts[0])).ToString();
                cmd.Parameters.AddWithValue("@series_end",
                    obj.SeriesEnd.Length <= 7 ? string.Format("{0}/{1}",lastday, obj.SeriesEnd) : obj.SeriesEnd);
            }

            cmd.Parameters.AddWithValue("@hsn", obj.HSN);
            cmd.Parameters.AddWithValue("@tsn", obj.TSN);
            cmd.Parameters.AddWithValue("@tsn2", obj.TSN2);
            cmd.Parameters.AddWithValue("@car_tax", obj.CarTax);
            cmd.Parameters.AddWithValue("@co2_class", obj.CO2Class);
            cmd.Parameters.AddWithValue("@base_price", obj.BasePrice);
            cmd.Parameters.AddWithValue("@engine_type", obj.EngineType);
            cmd.Parameters.AddWithValue("@fuel", obj.Fuel);
            cmd.Parameters.AddWithValue("@fuel2", obj.Fuel2);
            cmd.Parameters.AddWithValue("@emission_control", obj.EmissionControl);
            cmd.Parameters.AddWithValue("@engine_design", obj.EngineDesign);
            cmd.Parameters.AddWithValue("@cylinder", obj.Cylinder);

            cmd.Parameters.AddWithValue("@fuel_type", obj.FuelType);
            cmd.Parameters.AddWithValue("@charge", obj.Charge);
            cmd.Parameters.AddWithValue("@valves", obj.Valves);
            cmd.Parameters.AddWithValue("@cubic", obj.Cubic);
            cmd.Parameters.AddWithValue("@power_kw", obj.PowerKW);
            cmd.Parameters.AddWithValue("@power_ps", obj.PowerPS);
            cmd.Parameters.AddWithValue("@max_power", obj.MaxPower);
            cmd.Parameters.AddWithValue("@turning_moment", obj.TurningMoment);
            cmd.Parameters.AddWithValue("@max_turning_moment", obj.MaxTurningMoment);
            cmd.Parameters.AddWithValue("@type_of_drive", obj.TypeOfDrive);

            cmd.Parameters.AddWithValue("@gearing", obj.Gearing);
            cmd.Parameters.AddWithValue("@gears", obj.Gears);
            cmd.Parameters.AddWithValue("@start_stop_automatic", obj.StartStopAutomatic);
            cmd.Parameters.AddWithValue("@emission_class", obj.EmissionClass);
            cmd.Parameters.AddWithValue("@length", obj.Length);
            cmd.Parameters.AddWithValue("@width", obj.Width);
            cmd.Parameters.AddWithValue("@height", obj.Height);
            cmd.Parameters.AddWithValue("@chassis", obj.Chassis);
            cmd.Parameters.AddWithValue("@doors", obj.Doors);
            cmd.Parameters.AddWithValue("@car_class", obj.CarClass);
            cmd.Parameters.AddWithValue("@seats", obj.Seats);
            cmd.Parameters.AddWithValue("@speed_up", obj.SpeedUp);
            cmd.Parameters.AddWithValue("@max_speed", obj.MaxSpeed);
            cmd.Parameters.AddWithValue("@tank", obj.Tank);
            cmd.Parameters.AddWithValue("@tank2", obj.Tank2);
        }

        private bool CarDetailsExists(string maker, string model, string type, string series)
        {
            bool result = false;
            try
            {
                string query = string.Format(
                    "SELECT Count(*) FROM {0} WHERE maker LIKE '{1}' AND mode LIKE '{2}' AND type LIKE '{3}' AND series LIKE '{4}'",
                    MySQLExporter.MODELTYPE_TABLE,
                    maker, model, type, series);
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
                    Core.Log.Error(string.Format("SQLCarDetails::CarDetailsExists : {0}", ex.Message));
                }
                else
                {
                    throw new Exception("SQLCarDetails::CarDetailsExists", ex);
                }

                return result;
            }
        }
    }
}
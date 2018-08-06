﻿using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using VehicleGrabberCore.DataObjects;
using VehicleGrabberCore.Exporter;

class SQLCarDetails
{
    private MySQLExporter _mySqlExporter;

    public SQLCarDetails(MySQLExporter mySqlExporter)
    {
        _mySqlExporter = mySqlExporter;
    }

    public void Add_CarDetails()
    {
        foreach (CarDetailsObj obj in _mySqlExporter.carDetailsObjList)
        {
            try
            {
                long makerId = SQLCarMaker.GetMakerId(obj.Maker);
                long modelId = SQLCarModel.GetModelId(obj.Model);
                long typeId = SQLCarModelType.GetModelTypeId(obj.Type);

                long id = GetCarDetailsId(obj.Maker, obj.Model, obj.Type, obj.Series);
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
                throw new Exception("SQLCarDetails::Add_CarDetails", ex);
            }
        }
    }

    public void Insert_CarDetails(CarDetailsObj obj, long makerId, long modelId, long typeId)
    {
        string query = string.Format("INSERT INTO {0} " +
                                     "(maker_id, model_id, modeltype_id, maker, model, type, series, internal_class_name, model_start, model_end, " +
                                     "series_start, series_end, hsn, tsn, tsn2, car_tax, co2_class, base_price, engine_type, fuel, fuel2, " +
                                     "emission_control, engine_design, cylinder, fuel_type, charge, valves, cubic, power_kw, power_ps, " +
                                     "max_power, turning_moment, max_turning_moment, type_of_drive, gearing, gears, start_stop_automatic, " +
                                     "emission_class, length, width, height, chassis, doors, car_class, seats, speed_up, max_speed, tank, tank2) " +
                                     "VALUES" +
                                     "(@maker_id, @model_id, @modeltype_id, @maker, @model, @type, @series, @internal_class_name, @model_start, @model_end, " +
                                     "@series_start, @series_end, @hsn, @tsn, @tsn2, @car_tax, @co2_class, @base_price, @engine_type, @fuel, @fuel2, " +
                                     "@emission_control, @engine_design, @cylinder, @fuel_type, @charge, @valves, @cubic, @power_kw, @power_ps, " +
                                     "@max_power, @turning_moment, @max_turning_moment, @type_of_drive, @gearing, @gears, @start_stop_automatic, " +
                                     "@emission_class, @length, @width, @height, @chassis, @doors, @car_class, @seats, @speed_up, @max_speed, @tank, @tank2)",
            MySQLExporter.DETAILS_TABLE);


        //open connection
        if (_mySqlExporter.OpenConnection() == true)
        {
            //create command and assign the query and connection from the constructor
            MySqlCommand cmd = new MySqlCommand(query, _mySqlExporter.connection);


            SetSQLParameters(obj, cmd, makerId, modelId, typeId);

            //Execute command
            cmd.ExecuteNonQuery();

            //close connection
            _mySqlExporter.CloseConnection();
        }
    }

    public void Update_CarDetails(CarDetailsObj obj, long makerId, long modelId, long typeId, long id)
    {
        string query = string.Format("UPDATE {0} SET " +
                                     "maker_id=@maker_id, " +
                                     "model_id=@model_id, " +
                                     "modeltype_id=@modeltype_id" +
                                     "maker=@maker" +
                                     "model=@model" +
                                     "type=@type" +
                                     "series=@series" +
                                     "internal_class_name=@internal_class_name" +
                                     "model_start=@model_start" +
                                     "model_end=@model_end" +
                                     "series_start=@series_start" +
                                     "series_end=@series_end" +
                                     "hsn=@hsn" +
                                     "tsn=@tsn" +
                                     "tsn2=@tsn2" +
                                     "car_tax=@car_tax" +
                                     "co2_class=@co2_class" +
                                     "base_price=@base_price" +
                                     "engine_type=@engine_type" +
                                     "fuel=@fuel" +
                                     "fuel2=@fuel2" +
                                     "emission_control=@emission_control" +
                                     "engine_design=@engine_design" +
                                     "cylinder=@cylinder" +
                                     "fuel_type=@fuel_type" +
                                     "charge=@charge" +
                                     "valves=@valves" +
                                     "cubic=@cubic" +
                                     "power_kw=@power_kw" +
                                     "power_ps=@power_ps" +
                                     "max_power=@max_power" +
                                     "turning_moment=@turning_moment" +
                                     "max_turning_moment=@max_turning_moment" +
                                     "type_of_drive=@type_of_drive" +
                                     "gearing=@gearing" +
                                     "gears=@gears" +
                                     "start_stop_automatic=@start_stop_automatic" +
                                     "emission_class=@emission_class" +
                                     "length=@length" +
                                     "width=@width" +
                                     "height=@height" +
                                     "chassis=@chassis" +
                                     "doors=@doors" +
                                     "car_class=@car_class" +
                                     "seats=@seats" +
                                     "speed_up=@speed_up" +
                                     "max_speed=@max_speed" +
                                     "tank=@tank" +
                                     "tank2=@tank2" +
                                     " WHERE id={1}", MySQLExporter.DETAILS_TABLE,
            id);







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

            SetSQLParameters(obj, cmd, makerId, modelId, typeId);


            //Execute query
            cmd.ExecuteNonQuery();

            //close connection
            _mySqlExporter.CloseConnection();
        }
    }


    public long GetCarDetailsId(string maker, string model, string type, string series)
    {
        try
        {
            string query = string.Format("SELECT id FROM {0} WHERE maker LIKE {1} AND mode LIKE {2} AND type LIKE {3} AND series LIKE {4}", MySQLExporter.MODELTYPE_TABLE,
                maker, model, type, series);
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
            throw new Exception("SQLCarDetails::GetCarDetailsId", ex);
        }
    }

    private static void SetSQLParameters(CarDetailsObj obj, MySqlCommand cmd, long makerId, long modelId, long typeId)
    {
        cmd.Parameters.AddWithValue("@maker_id", makerId);
        cmd.Parameters.AddWithValue("@model_id", modelId);
        cmd.Parameters.AddWithValue("@modeltype_id", typeId);
        cmd.Parameters.AddWithValue("@maker", obj.Maker);
        cmd.Parameters.AddWithValue("@model", obj.Model);
        cmd.Parameters.AddWithValue("@type", obj.Type);
        cmd.Parameters.AddWithValue("@series", obj.Series);
        cmd.Parameters.AddWithValue("@internal_class_name", obj.InternalClassName);

        //dateTime ?
        cmd.Parameters.AddWithValue("@model_start", obj.ModelStart);
        cmd.Parameters.AddWithValue("@model_end", obj.ModelEnd);
        cmd.Parameters.AddWithValue("@series_start", obj.SeriesStart);
        cmd.Parameters.AddWithValue("@series_end", obj.SeriesEnd);

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
            string query = string.Format("SELECT Count(*) FROM {0} WHERE maker LIKE {1} AND mode LIKE {2} AND type LIKE {3} AND series LIKE {4}", MySQLExporter.MODELTYPE_TABLE,
                maker, model, type, series);
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
            throw new Exception("SQLCarDetails::CarDetailsExists", ex);
        }
    }
}

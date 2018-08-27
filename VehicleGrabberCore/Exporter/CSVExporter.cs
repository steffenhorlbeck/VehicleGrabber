using FileHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VehicleGrabberCore.Configuration;
using VehicleGrabberCore.DataObjects;


namespace VehicleGrabberCore.Exporter
{
    class CSVExporter
    {
        public const string ROOT = "C:\\KFZ";
        public const string MODELS = "models";
        public const string MAKERS = "makers";
        public const string DELIMITER = ";";
        private readonly string modelsFileName = "model.csv";
        private readonly string modelTypesFileName = "modeltypes.csv";
        private readonly string carDetailsFileName = "cardetails.csv";
        public List<MakerObj> MakersList { get; }
        public List<ModelObj> ModelObjList { get; }
        public List<ModelTypeObj> ModelTypeObjList { get; }
        public List<CarDetailsObj> CarDetailsObjList { get; }
        public VGCore Core { get; set; }

        public CSVExporter(List<MakerObj> MakerObjects, List<ModelObj> modelsObjects, List<ModelTypeObj> modelTypesObjects, List<CarDetailsObj> carDetailsObjects, string modelsFile = "", string typesFile = "")
        {
            Directory.CreateDirectory(Path.Combine(ROOT, MODELS));
            Directory.CreateDirectory(Path.Combine(ROOT, MAKERS));

            if (!string.IsNullOrWhiteSpace(modelsFile))
            {
                modelsFileName = modelsFile;
            }

            if (!string.IsNullOrWhiteSpace(typesFile))
            {
                modelTypesFileName = typesFile;
            }

            this.MakersList = MakerObjects;
            this.ModelObjList = modelsObjects;
            this.ModelTypeObjList = modelTypesObjects;
            this.CarDetailsObjList = carDetailsObjects;
        }

        public void ExportModels()
        {
            var engine = new FileHelperEngine<ModelsClass>(Encoding.GetEncoding(this.Core.Conf.DefaultCSVEncoding))
            {
                HeaderText =
                    typeof(ModelsClass)
                        .GetCsvHeader()
            };


            

            var models = new List<ModelsClass>();

            foreach(ModelObj model in ModelObjList)
            {
                models.Add(new ModelsClass()
                {
                    ModelId = model.ModelID,
                    ModelName = model.ModelName,
                    ModelUrlPath = model.ModelUrlPath,
                    ModelImgBase64 = model.ModelImgBase64,
                    ModelThumbUrl = model.ModelThumbUrl,
                    MakerName = model.MakerName,
                    MakerUrlPath = model.MakerUrlPath,
                    MakerLogoBase64 = model.MakerLogoBase64,
                    MakerLogoUrl = model.MakerLogoUrl
                });
            }

            try
            {
                Directory.CreateDirectory(ROOT);

                string fileName = String.Empty;
                if (!string.IsNullOrWhiteSpace(this.Core.Conf.MakerName))
                {
                    fileName = Path.Combine(ROOT, string.Format("{0}.{1}",this.Core.Conf.MakerName, this.Core.Conf.DefaultCSVFileExtension));
                }
                else
                {
                    fileName = Path.Combine(ROOT, this.modelsFileName);
                }

                engine.WriteFile(fileName, models);
            }
            catch (Exception ex)
            {
                this.Core.Log.Error(string.Format("CSVExporter::ExportModels : {0}", ex.Message));
            }
        }

        public void ExportModelTypes()
        {
            //var engine = new FileHelperEngine<ModelTypesClass>(Encoding.UTF8);
            var engine = new FileHelperEngine<ModelTypesClass>(Encoding.GetEncoding(this.Core.Conf.DefaultCSVEncoding))
            {
                HeaderText =
                    typeof(ModelsClass)
                        .GetCsvHeader()
            };

            var modeltypes = new List<ModelTypesClass>();

            foreach (ModelTypeObj type in ModelTypeObjList)
            {
                modeltypes.Add(new ModelTypesClass()
                {
                    ModelId = type.ModelID,
                    ModelTypeId = type.ModelTypeID,
                    ModelTypeName = type.ModelTypeName,
                    ModelTypeCubic = type.ModelTypeCubic,
                    ModelTypeFuel = type.ModelTypeFuel,
                    ModelTypePower = type.ModelTypePower,
                    ModelTypeTank = type.ModelTypeTank,
                    ModelTypeFromYear = type.ModelTypeFromYear,
                    ModelTypeToYear = type.ModelTypeToYear,
                    ModelTypeDetailsUrl = type.ModelTypeDetailsUrl,
                    ModelTypeChassis = type.ModelTypeChassis,
                    ModelTypeDoors = type.ModelTypeDoors
                });
            }

            try
            {
                Directory.CreateDirectory(ROOT);
                string fileName = Path.Combine(ROOT, this.modelTypesFileName);
                engine.WriteFile(fileName, modeltypes);
            }
            catch (Exception ex)
            {
                this.Core.Log.Error(string.Format("CSVExporter::ExportModelTypes : {0}", ex.Message));
            }
        }


        public void ExportCarDetails()
        {
            //var engine = new FileHelperEngine<CarDetailsClass>(Encoding.UTF8);

            var engine = new FileHelperEngine<CarDetailsClass>(Encoding.GetEncoding(this.Core.Conf.DefaultCSVEncoding))
            {
                HeaderText =
                    typeof(CarDetailsClass)
                        .GetCsvHeader()
            };

            var cardetails = new List<CarDetailsClass>();

            foreach (CarDetailsObj car in CarDetailsObjList)
            {
                cardetails.Add(new CarDetailsClass()
                {
                    ModelId = car.ModelID,
                    ModelTypeId = car.ModelTypeID,
                    Maker = car.Maker, // Marke VW
                    Model = car.Model, //Modell up! 1.0
                    Type = car.Type, //Typ take up!
                    Series = car.Series, //Baureihe up!
                    InternalClassName = car.InternalClassName, //Herstellerinterne Baureihenbezeichnung  AA
                    ModelStart = car.ModelStart, //Modellstart	06/2016
                    ModelEnd = car.ModelEnd, //Modellende
                    SeriesStart = car.SeriesStart, //Baureihenstart	06/2016
                    SeriesEnd = car.SeriesEnd, //Baureihenende
                    HSN = car.HSN, //HSN Schlüsselnummer	0603
                    TSN = car.TSN, //TSN Schlüsselnummer BGU
                    TSN2 = car.TSN2, //TSN Schlüsselnummer 2	
                    CarTax = car.CarTax, //KFZ-Steuer pro Jahr	32 Euro
                    CO2Class = car.CO2Class, //CO2-Effizienzklasse C
                    BasePrice = car.BasePrice, //Grundpreis	9975 Euro

                    //Motor und Antrieb
                    EngineType = car.EngineType, //Motorart Otto
                    Fuel = car.Fuel, //Kraftstoffart Super
                    Fuel2 = car.Fuel2, //Kraftstoffart(2.Antrieb)   -
                    EmissionControl = car.EmissionControl, //Abgasreinigung geregelt
                    EngineDesign = car.EngineDesign, //Motorbauart Reihe
                    Cylinder = car.Cylinder, //Anzahl Zylinder	3
                    FuelType = car.FuelType, //Gemischaufbereitung Einspritzung
                    Charge = car.Charge, //Aufladung keine Aufladung
                    Valves = car.Valves, //Anzahl Ventile	4
                    Cubic = car.Cubic, //Hubraum	999 ccm
                    PowerKW = car.PowerKW, //Leistung in kW	44
                    PowerPS = car.PowerPS, //Leistung in PS	60
                    MaxPower = car.MaxPower, //Leistung maximal bei U/min. 5000 U/min
                    TurningMoment = car.TurningMoment, //Drehmoment	95 Nm
                    MaxTurningMoment = car.MaxTurningMoment, //Drehmoment maximal bei U/min.   3000 U/min
                    TypeOfDrive = car.TypeOfDrive, //Antriebsart Front
                    Gearing = car.Gearing, //Getriebeart Schaltgetriebe
                    Gears = car.Gears, //Anzahl Gänge	5
                    StartStopAutomatic = car.StartStopAutomatic, //Start-/Stopp-Automatik	-
                    EmissionClass = car.EmissionClass, //Schadstoffklasse Euro 6b

                    //Maße und Gewichte
                    Length = car.Length, //Länge	3600 mm
                    Width = car.Width, //Breite	1645 mm
                    Height = car.Height, //Höhe	1504 mm

                    //Karosserie und Fahrwerk
                    Chassis = car.Chassis, //Karosserie Schrägheck
                    Doors = car.Doors, //Türanzahl   3
                    CarClass = car.CarClass, //Fahrzeugklasse  Kleinstwagen (z.B.Twingo)
                    Seats = car.Seats, //Sitzanzahl	4

                    SpeedUp = car.SpeedUp, // Beschleunigung
                    MaxSpeed = car.MaxSpeed, // Höchstgeschwindigkeit
                    Tank = car.Tank, //Tankgröße	35 l
                    Tank2 = car.Tank2 //Tankgröße(2.Antrieb)   -
                });
            }

            try
            {
                Directory.CreateDirectory(ROOT);
                string fileName = Path.Combine(ROOT, this.carDetailsFileName);
                engine.WriteFile(fileName, cardetails);
            }
            catch (Exception ex)
            {
                this.Core.Log.Error(string.Format("CSVExporter::ExportCarDetails : {0}", ex.Message));
            }
        }

    }
}

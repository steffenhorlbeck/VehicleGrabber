using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleGrabberCore.DataObjects
{
    public class CarDetailsObj
    {

        public CarDetailsObj()
        {
            ModelID = -1;
            ModelTypeID = -1;
            Maker = string.Empty; // Marke VW
            Model= string.Empty; //Modell up! 1.0
            Type= string.Empty; //Typ take up!
            Series= string.Empty; //Baureihe up!
            ModelTypeName = string.Empty;
            InternalClassName= string.Empty; //Herstellerinterne Baureihenbezeichnung  AA
            ModelStart= string.Empty; //Modellstart	06/2016
            ModelEnd= string.Empty; //Modellende
            SeriesStart= string.Empty; //Baureihenstart	06/2016
            SeriesEnd= string.Empty; //Baureihenende
            HSN= string.Empty; //HSN Schlüsselnummer	0603
            TSN= string.Empty; //TSN Schlüsselnummer BGU
            TSN2= string.Empty; //TSN Schlüsselnummer 2	
            CarTax= string.Empty; //KFZ-Steuer pro Jahr	32 Euro
            CO2Class= string.Empty; //CO2-Effizienzklasse C
            BasePrice= string.Empty; //Grundpreis	9975 Euro

            //Motor und Antrieb
            EngineType= string.Empty; //Motorart Otto
            Fuel= string.Empty; //Kraftstoffart Super
            Fuel2= string.Empty; //Kraftstoffart(2.Antrieb)   -
            EmissionControl= string.Empty; //Abgasreinigung geregelt
            EngineDesign= string.Empty; //Motorbauart Reihe
            Cylinder= 0; //Anzahl Zylinder	3
            FuelType= string.Empty; //Gemischaufbereitung Einspritzung
            Charge= string.Empty; //Aufladung keine Aufladung
            Valves= 0; //Anzahl Ventile	4
            Cubic= string.Empty; //Hubraum	999 ccm
            PowerKW= 0; //Leistung in kW	44
            PowerPS= 0; //Leistung in PS	60
            MaxPower= string.Empty; //Leistung maximal bei U/min. 5000 U/min
            TurningMoment= string.Empty; //Drehmoment	95 Nm
            MaxTurningMoment= string.Empty; //Drehmoment maximal bei U/min.   3000 U/min
            TypeOfDrive= string.Empty; //Antriebsart Front
            Gearing= string.Empty; //Getriebeart Schaltgetriebe
            Gears= 0; //Anzahl Gänge	5
            StartStopAutomatic= string.Empty; //Start-/Stopp-Automatik	-
            EmissionClass= string.Empty; //Schadstoffklasse Euro 6b

            //Maße und Gewichte
            Length= string.Empty; //Länge	3600 mm
            Width= string.Empty; //Breite	1645 mm
            Height= string.Empty; //Höhe	1504 mm

            //Karosserie und Fahrwerk
            Chassis= string.Empty; //Karosserie Schrägheck
            Doors= 0; //Türanzahl   3
            CarClass= string.Empty; //Fahrzeugklasse  Kleinstwagen (z.B.Twingo)
            Seats= 0; //Sitzanzahl	4

            Tank= string.Empty; //Tankgröße	35 l
            Tank2= string.Empty; //Tankgröße(2.Antrieb)   -
        }

        public long ModelID { get; set; }
        public long ModelTypeID { get; set; }


        public string Maker { get; set; } // Marke VW
        public string Model { get; set; } //Modell up! 1.0
        public string Type { get; set; } //Typ take up!
        public string Series { get; set; } //Baureihe up!
        public string ModelTypeName { get; set; }
        public string InternalClassName { get; set; } //Herstellerinterne Baureihenbezeichnung  AA
        public string ModelStart { get; set; } //Modellstart	06/2016
        public string ModelEnd { get; set; } //Modellende
        public string SeriesStart { get; set; } //Baureihenstart	06/2016
        public string SeriesEnd { get; set; } //Baureihenende
        public string HSN { get; set; } //HSN Schlüsselnummer	0603
        public string TSN { get; set; } //TSN Schlüsselnummer BGU
        public string TSN2 { get; set; } //TSN Schlüsselnummer 2	
        public string CarTax { get; set; } //KFZ-Steuer pro Jahr	32 Euro
        public string CO2Class { get; set; } //CO2-Effizienzklasse C
        public string BasePrice { get; set; } //Grundpreis	9975 Euro

        //Motor und Antrieb
        public string EngineType { get; set; } //Motorart Otto
        public string Fuel { get; set; } //Kraftstoffart Super
        public string Fuel2 { get; set; } //Kraftstoffart(2.Antrieb)   -
        public string EmissionControl { get; set; } //Abgasreinigung geregelt
        public string EngineDesign { get; set; } //Motorbauart Reihe
        public int Cylinder { get; set; } //Anzahl Zylinder	3
        public string FuelType { get; set; } //Gemischaufbereitung Einspritzung
        public string Charge { get; set; } //Aufladung keine Aufladung
        public int Valves { get; set; } //Anzahl Ventile	4
        public string Cubic { get; set; } //Hubraum	999 ccm
        public int PowerKW { get; set; } //Leistung in kW	44
        public int PowerPS { get; set; } //Leistung in PS	60
        public string MaxPower { get; set; } //Leistung maximal bei U/min. 5000 U/min
        public string TurningMoment { get; set; } //Drehmoment	95 Nm
        public string MaxTurningMoment { get; set; } //Drehmoment maximal bei U/min.   3000 U/min
        public string TypeOfDrive { get; set; } //Antriebsart Front
        public string Gearing { get; set; } //Getriebeart Schaltgetriebe
        public int Gears { get; set; } //Anzahl Gänge	5
        public string StartStopAutomatic { get; set; } //Start-/Stopp-Automatik	-
        public string EmissionClass { get; set; } //Schadstoffklasse Euro 6b

        //Maße und Gewichte
        public string Length { get; set; } //Länge	3600 mm
        public string Width { get; set; } //Breite	1645 mm
        public string Height { get; set; } //Höhe	1504 mm

        //Karosserie und Fahrwerk
        public string Chassis { get; set; } //Karosserie Schrägheck
        public int Doors { get; set; } //Türanzahl   3
        public string CarClass { get; set; } //Fahrzeugklasse  Kleinstwagen (z.B.Twingo)
        public int Seats { get; set; } //Sitzanzahl	4

        public string SpeedUp { get; set; } // Beschleunigung
        public string MaxSpeed { get; set; } // Höchstgeschwindigkeit
        public string Tank { get; set; } //Tankgröße	35 l
        public string Tank2 { get; set; } //Tankgröße(2.Antrieb)   -
        
    }
}

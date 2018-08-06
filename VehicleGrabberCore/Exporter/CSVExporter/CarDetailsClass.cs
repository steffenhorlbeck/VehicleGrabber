using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleGrabberCore.Exporter;


[DelimitedRecord(CSVExporter.DELIMITER)]
public class CarDetailsClass
{
    [FieldOrder(1), FieldTitle("ModelId")]
    public long ModelId;

    [FieldOrder(2), FieldTitle("ModelTypeId")]
    public long ModelTypeId;


    [FieldOrder(10), FieldTitle("Maker")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Maker; // Marke VW

    [FieldOrder(20), FieldTitle("Model")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Model; //Modell up! 1.0

    [FieldOrder(30), FieldTitle("Type")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Type; //Typ take up!

    [FieldOrder(40), FieldTitle("Series")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Series; //Baureihe up!

    [FieldOrder(50), FieldTitle("InternalClassName")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string InternalClassName; //Herstellerinterne Baureihenbezeichnung  AA

    [FieldOrder(60), FieldTitle("ModelStart")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelStart; //Modellstart	06/2016

    [FieldOrder(70), FieldTitle("ModelEnd")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelEnd; //Modellende

    [FieldOrder(80), FieldTitle("SeriesStart")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string SeriesStart; //Baureihenstart	06/2016

    [FieldOrder(90), FieldTitle("SeriesEnd")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string SeriesEnd; //Baureihenende

    [FieldOrder(100), FieldTitle("HSN")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string HSN; //HSN Schlüsselnummer	0603

    [FieldOrder(110), FieldTitle("TSN")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string TSN; //TSN Schlüsselnummer BGU

    [FieldOrder(120), FieldTitle("TSN2")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string TSN2; //TSN Schlüsselnummer 2	

    [FieldOrder(130), FieldTitle("CarTax")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string CarTax; //KFZ-Steuer pro Jahr	32 Euro

    [FieldOrder(140), FieldTitle("CO2Class")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string CO2Class; //CO2-Effizienzklasse C

    [FieldOrder(150), FieldTitle("BasePrice")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string BasePrice; //Grundpreis	9975 Euro

    //Motor und Antrieb
    [FieldOrder(160), FieldTitle("EngineType")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string EngineType; //Motorart Otto

    [FieldOrder(170), FieldTitle("Fuel")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Fuel; //Kraftstoffart Super

    [FieldOrder(180), FieldTitle("Fuel2")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Fuel2; //Kraftstoffart(2.Antrieb)   -

    [FieldOrder(190), FieldTitle("EmissionControl")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string EmissionControl; //Abgasreinigung geregelt

    [FieldOrder(200), FieldTitle("EngineDesign")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string EngineDesign; //Motorbauart Reihe

    [FieldOrder(210), FieldTitle("Cylinder")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public int Cylinder; //Anzahl Zylinder	3

    [FieldOrder(220), FieldTitle("FuelType")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string FuelType; //Gemischaufbereitung Einspritzung

    [FieldOrder(230), FieldTitle("Charge")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Charge; //Aufladung keine Aufladung

    [FieldOrder(240), FieldTitle("Valves")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public int Valves; //Anzahl Ventile	4

    [FieldOrder(250), FieldTitle("Cubic")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Cubic; //Hubraum	999 ccm

    [FieldOrder(260), FieldTitle("PowerKW")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public int PowerKW; //Leistung in kW	44

    [FieldOrder(270), FieldTitle("PowerPS")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public int PowerPS; //Leistung in PS	60

    [FieldOrder(280), FieldTitle("MaxPower")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string MaxPower; //Leistung maximal bei U/min. 5000 U/min

    [FieldOrder(290), FieldTitle("TurningMoment")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string TurningMoment; //Drehmoment	95 Nm

    [FieldOrder(300), FieldTitle("MaxTurningMoment")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string MaxTurningMoment; //Drehmoment maximal bei U/min.   3000 U/min

    [FieldOrder(310), FieldTitle("TypeOfDrive")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string TypeOfDrive; //Antriebsart Front

    [FieldOrder(320), FieldTitle("Gearing")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Gearing; //Getriebeart Schaltgetriebe

    [FieldOrder(330), FieldTitle("Gears")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public int Gears; //Anzahl Gänge	5

    [FieldOrder(340), FieldTitle("StartStopAutomatic")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string StartStopAutomatic; //Start-/Stopp-Automatik	-

    [FieldOrder(350), FieldTitle("EmissionClass")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string EmissionClass; //Schadstoffklasse Euro 6b

    //Maße und Gewichte
    [FieldOrder(360), FieldTitle("Length")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Length; //Länge	3600 mm

    [FieldOrder(370), FieldTitle("Width")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Width; //Breite	1645 mm

    [FieldOrder(380), FieldTitle("Height")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Height; //Höhe	1504 mm

    //Karosserie und Fahrwerk
    [FieldOrder(390), FieldTitle("Chassis")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Chassis; //Karosserie Schrägheck

    [FieldOrder(400), FieldTitle("Doors")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public int Doors; //Türanzahl   3

    [FieldOrder(410), FieldTitle("CarClass")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string CarClass; //Fahrzeugklasse  Kleinstwagen (z.B.Twingo)

    [FieldOrder(420), FieldTitle("Seats")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public int Seats; //Sitzanzahl	4

    [FieldOrder(430), FieldTitle("SpeedUp")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string SpeedUp; //Tankgröße	35 l

    [FieldOrder(440), FieldTitle("MaxSpeed")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string MaxSpeed; //Tankgröße(2.Antrieb)   -

    [FieldOrder(450), FieldTitle("Tank")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Tank; //Tankgröße	35 l

    [FieldOrder(460), FieldTitle("Tank2")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Tank2; //Tankgröße(2.Antrieb)   -
}


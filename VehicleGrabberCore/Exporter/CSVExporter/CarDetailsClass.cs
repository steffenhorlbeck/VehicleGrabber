using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;


[DelimitedRecord(";")]
public class CarDetailsClass
{
    [FieldOrder(1)]
    public long ModelId;

    [FieldOrder(2)]
    public long ModelTypeId;


    [FieldOrder(10)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Maker; // Marke VW

    [FieldOrder(20)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Model; //Modell up! 1.0

    [FieldOrder(30)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Type; //Typ take up!

    [FieldOrder(40)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Series; //Baureihe up!

    [FieldOrder(50)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string InternalClassName; //Herstellerinterne Baureihenbezeichnung  AA

    [FieldOrder(60)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelStart; //Modellstart	06/2016

    [FieldOrder(70)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelEnd; //Modellende

    [FieldOrder(80)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string SeriesStart; //Baureihenstart	06/2016

    [FieldOrder(90)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string SeriesEnd; //Baureihenende

    [FieldOrder(100)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string HSN; //HSN Schlüsselnummer	0603

    [FieldOrder(110)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string TSN; //TSN Schlüsselnummer BGU

    [FieldOrder(120)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string TSN2; //TSN Schlüsselnummer 2	

    [FieldOrder(130)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string CarTax; //KFZ-Steuer pro Jahr	32 Euro

    [FieldOrder(140)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string CO2Class; //CO2-Effizienzklasse C

    [FieldOrder(150)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string BasePrice; //Grundpreis	9975 Euro

    //Motor und Antrieb
    [FieldOrder(160)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string EngineType; //Motorart Otto

    [FieldOrder(170)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Fuel; //Kraftstoffart Super

    [FieldOrder(180)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Fuel2; //Kraftstoffart(2.Antrieb)   -

    [FieldOrder(190)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string EmissionControl; //Abgasreinigung geregelt

    [FieldOrder(200)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string EngineDesign; //Motorbauart Reihe

    [FieldOrder(210)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public int Cylinder; //Anzahl Zylinder	3

    [FieldOrder(220)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string FuelType; //Gemischaufbereitung Einspritzung

    [FieldOrder(230)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Charge; //Aufladung keine Aufladung

    [FieldOrder(240)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public int Valves; //Anzahl Ventile	4

    [FieldOrder(250)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Cubic; //Hubraum	999 ccm

    [FieldOrder(260)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public int PowerKW; //Leistung in kW	44

    [FieldOrder(270)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public int PowerPS; //Leistung in PS	60

    [FieldOrder(280)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string MaxPower; //Leistung maximal bei U/min. 5000 U/min

    [FieldOrder(290)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string TurningMoment; //Drehmoment	95 Nm

    [FieldOrder(300)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string MaxTurningMoment; //Drehmoment maximal bei U/min.   3000 U/min

    [FieldOrder(310)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string TypeOfDrive; //Antriebsart Front

    [FieldOrder(320)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Gearing; //Getriebeart Schaltgetriebe

    [FieldOrder(330)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public int Gears; //Anzahl Gänge	5

    [FieldOrder(340)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string StartStopAutomatic; //Start-/Stopp-Automatik	-

    [FieldOrder(350)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string EmissionClass; //Schadstoffklasse Euro 6b

    //Maße und Gewichte
    [FieldOrder(360)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Length; //Länge	3600 mm

    [FieldOrder(370)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Width; //Breite	1645 mm

    [FieldOrder(380)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Height; //Höhe	1504 mm

    //Karosserie und Fahrwerk
    [FieldOrder(390)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Chassis; //Karosserie Schrägheck

    [FieldOrder(400)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public int Doors; //Türanzahl   3

    [FieldOrder(410)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string CarClass; //Fahrzeugklasse  Kleinstwagen (z.B.Twingo)

    [FieldOrder(420)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public int Seats; //Sitzanzahl	4

    [FieldOrder(430)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string SpeedUp; //Tankgröße	35 l

    [FieldOrder(440)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string MaxSpeed; //Tankgröße(2.Antrieb)   -

    [FieldOrder(450)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Tank; //Tankgröße	35 l

    [FieldOrder(460)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string Tank2; //Tankgröße(2.Antrieb)   -
}


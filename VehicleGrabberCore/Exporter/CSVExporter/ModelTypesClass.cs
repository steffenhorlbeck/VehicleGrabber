using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleGrabberCore.Exporter;


[DelimitedRecord(CSVExporter.DELIMITER)]
public class ModelTypesClass
{
    [FieldOrder(1), FieldTitle("ModelId")]
    public long ModelId;

    [FieldOrder(2), FieldTitle("ModelTypeId")]
    public long ModelTypeId;

    [FieldOrder(10), FieldTitle("ModelTypeName")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelTypeName;

    [FieldOrder(20), FieldTitle("ModelTypeCubic")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelTypeCubic;

    [FieldOrder(30), FieldTitle("ModelTypeFuel")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelTypeFuel;

    [FieldOrder(40), FieldTitle("ModelTypePower")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelTypePower;

    [FieldOrder(50), FieldTitle("ModelTypeTank")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelTypeTank;

    [FieldOrder(60), FieldTitle("ModelTypeFromYear")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelTypeFromYear;

    [FieldOrder(70), FieldTitle("ModelTypeToYear")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelTypeToYear;

    [FieldOrder(80), FieldTitle("ModelTypeDetailsUrl")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelTypeDetailsUrl;

    [FieldOrder(90), FieldTitle("ModelTypeChassis")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelTypeChassis;

    [FieldOrder(100), FieldTitle("ModelTypeDoors")]
    [FieldTrim(TrimMode.Both)]    
    public int ModelTypeDoors;
}

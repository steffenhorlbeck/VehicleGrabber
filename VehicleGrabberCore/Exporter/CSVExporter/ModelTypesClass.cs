using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;


[DelimitedRecord(";")]
public class ModelTypesClass
{
    [FieldOrder(1)]
    public long ModelId;

    [FieldOrder(2)]
    public long ModelTypeId;

    [FieldOrder(10)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelTypeName;

    [FieldOrder(20)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelTypeCubic;

    [FieldOrder(30)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelTypeFuel;

    [FieldOrder(40)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelTypePower;

    [FieldOrder(50)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelTypeTank;

    [FieldOrder(60)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelTypeFromYear;

    [FieldOrder(70)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelTypeToYear;

    [FieldOrder(80)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelTypeDetailsUrl;

    [FieldOrder(90)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelTypeChassis;

    [FieldOrder(100)]
    [FieldTrim(TrimMode.Both)]    
    public int ModelTypeDoors;
}

/// <summary>
/// Layout for a file delimited by |
/// </summary>
/// 
using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleGrabberCore.Exporter;


[DelimitedRecord(CSVExporter.DELIMITER)]
public class ModelsClass
{
    [FieldOrder(1), FieldTitle("ModelId")]
    public long ModelId;

    [FieldOrder(10), FieldTitle("ModelName")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelName;

    [FieldOrder(20), FieldTitle("ModelUrlPath")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelUrlPath;

    [FieldOrder(30), FieldTitle("ModelImgBase64")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelImgBase64;

    [FieldOrder(40), FieldTitle("ModelThumbUrl")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelThumbUrl;

    [FieldOrder(50), FieldTitle("MakerName")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string MakerName;

    [FieldOrder(60), FieldTitle("MakerUrlPath")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string MakerUrlPath;

    [FieldOrder(70), FieldTitle("MakerLogoBase64")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string MakerLogoBase64;

    [FieldOrder(80), FieldTitle("MakerLogoUrl")]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string MakerLogoUrl;
}



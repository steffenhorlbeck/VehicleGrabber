/// <summary>
/// Layout for a file delimited by |
/// </summary>
/// 
using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;



[DelimitedRecord(";")]
public class ModelsClass
{
    [FieldOrder(1)]
    public long ModelId;

    [FieldOrder(10)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelName;

    [FieldOrder(20)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelUrlPath;

    [FieldOrder(30)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelImgBase64;

    [FieldOrder(40)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string ModelThumbUrl;

    [FieldOrder(50)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string MakerName;

    [FieldOrder(60)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string MakerUrlPath;

    [FieldOrder(70)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string MakerLogoBase64;

    [FieldOrder(80)]
    [FieldTrim(TrimMode.Both)]
    [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
    public string MakerLogoUrl;
}



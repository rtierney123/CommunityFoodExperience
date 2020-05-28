using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class ColorDisplayer
{
    private readonly string standardHex = "#000000";
    private readonly string successHex = "#2A840C";

    private Color standardColor;
    private Color successColor;
    public ColorDisplayer()
    {
        ColorUtility.TryParseHtmlString(standardHex, out standardColor);
        ColorUtility.TryParseHtmlString(successHex, out successColor);
    }

    public void setStandard(Text text)
    {
        text.color = standardColor;
    }

    public void setSuccess(Text text)
    {
        text.color = successColor;
    }
}

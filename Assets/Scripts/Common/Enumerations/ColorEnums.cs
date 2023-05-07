using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorEnums : Enumeration
{
    public ColorEnums(int id, Color32 color) : base(id, color.ToString()) { colors = color; }

    private Color32 colors;

    public ColorEnums GreyFont = new ColorEnums(0, new Color32(180, 180, 180, 255));
}

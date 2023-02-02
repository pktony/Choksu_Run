using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/choosing-between-class-and-struct
/// </summary>
[Serializable]
public class PlayerDatas
{
    public int gold;
    public List<bool> characterUnlockInfo;

}

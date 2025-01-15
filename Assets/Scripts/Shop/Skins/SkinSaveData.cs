using System;
using UnityEngine;

[Serializable]
public class SkinSaveData
{
    public bool WasBought;
    public bool IsActive;

    public SkinSaveData(Skin skin)
    {
        WasBought = skin.WasBought;
        IsActive = skin.IsActive;
    }
}
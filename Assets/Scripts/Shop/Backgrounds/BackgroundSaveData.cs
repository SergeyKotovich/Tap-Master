using System;

[Serializable]
public class BackgroundSaveData
{
    public bool WasBought;
    public bool IsActive;
    public BackgroundSaveData(Background background)
    {
        WasBought = background.WasBought;
        IsActive = background.IsActive;
    }
}
using System;

[Serializable]
public class BoosterSaveData
{
    public bool WasBought;
    public int Count;

    public BoosterSaveData(Booster booster)
    {
        WasBought = booster.WasBought;
        Count = booster.Count;
    }
}
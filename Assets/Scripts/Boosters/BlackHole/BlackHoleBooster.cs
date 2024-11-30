using System;


public class BlackHoleBooster : IBooster
{
    public event Action<int, IBooster> WasBought;
    
    public BoostersType Type { get; }
    public int Price { get; }
   

    public BlackHoleBooster(BoostersType type, int cost)
    {
        Type = type;
        Price = cost;
    }
}


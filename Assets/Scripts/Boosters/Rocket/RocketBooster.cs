using System;

public class RocketBooster : IBooster
{
    public event Action<int, IBooster> WasBought;
    public BoostersType Type { get; }
    public int Price { get; }
    
    public RocketBooster(BoostersType type, int cost)
    {
        Type = type;
        Price = cost;
    }
}
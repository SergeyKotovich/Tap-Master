using System;


public class LaserBooster : IBooster
{
    public event Action<int, IBooster> WasBought;
    public BoostersType Type { get; }
    public int Price { get; }

    public LaserBooster(BoostersType type, int cost)
    {
        Type = type;
        Price = cost;
    }
}
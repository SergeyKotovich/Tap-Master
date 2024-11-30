using System;


public interface IBooster
{
    public event Action<int, IBooster> WasBought;
    public BoostersType Type { get; }
    public int Price { get; }
    
}
using System;
using UnityEngine;

public class LaserBooster : Booster
{
    public bool WasBought { get; private set; }
    public BoostersType Type { get; private set; }
    public int Price { get; private set; }
    public int Count { get; private set; }
    
    public override void Initialize(ShopConfig shopConfig)
    {
        Type = BoostersType.Laser;
        Price = shopConfig.CostLaser;
    }
    
    public override void AddBooster()
    {
        Count++;
    }

    public override void SpendBooster()
    {
        Count--;
    }
}
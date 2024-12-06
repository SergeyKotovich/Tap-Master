using System;
using UnityEngine;

public class LaserBooster : Booster
{
    public override void Initialize(ShopConfig shopConfig)
    {
        Type = BoostersType.Laser;
        Price = shopConfig.CostLaser;
    }
}
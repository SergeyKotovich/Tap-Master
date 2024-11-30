using System.Collections.Generic;
using UnityEngine;


public class ShopController : MonoBehaviour
{
    [SerializeField] private int _costBlackHole;
    [SerializeField] private int _costLaser;
    [SerializeField] private int _costRocket;
    
    private List<IBooster> _boosters = new();

    private void Awake()
    {
        _boosters.Add(new BlackHoleBooster(BoostersType.BlackHole, _costBlackHole));
        _boosters.Add(new LaserBooster(BoostersType.Laser, _costLaser));
        _boosters.Add(new RocketBooster(BoostersType.Rocket, _costRocket));
    }
}
using JetBrains.Annotations;
using UnityEngine;
using VContainer;


public class Player : MonoBehaviour
{
    private IInventoryHandler _inventory;

    [Inject]
    public void Construct(IInventoryHandler inventory)
    {
        _inventory = inventory;
    }

    [UsedImplicitly]
    public void SpendBooster(Booster booster)
    {
        _inventory.UseBooster(booster);
    }
    
}
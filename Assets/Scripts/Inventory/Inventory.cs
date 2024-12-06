using System;
using System.Collections.Generic;
using System.Linq;

public class Inventory : IInventoryHandler
{
    public event Action <Booster> CountBoostersChanged;
    public event Action<Booster> BoostersDepleted;
    private List<Booster> Boosters { get; }

    public Inventory(List<Booster> boosters)
    {
        Boosters = boosters;
    }
    public void AddBooster(Booster booster)
    {
        if (Boosters.Any(item => booster.Type == item.Type))
        {
            booster.AddBooster();
            CountBoostersChanged?.Invoke(booster);
        }
    }

    public void UseBooster(Booster booster)
    {
        if (Boosters.Any(item =>booster.Type == item.Type))
        {
            if (!booster.HasBooster())
            {
                return;
            }
            booster.SpendBooster();
            CountBoostersChanged?.Invoke(booster);
            if (!booster.HasBooster())
            {
                BoostersDepleted?.Invoke(booster);
            }
        }
    }
}
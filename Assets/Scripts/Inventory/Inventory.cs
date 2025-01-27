using System;
using System.Collections.Generic;
using System.Linq;

public class Inventory : IInventoryHandler
{
    public event Action<Booster> CountBoostersChanged;
    public event Action<Booster> BoostersDepleted;

    private readonly List<Booster> _boosters;

    private readonly BackgroundsLoader _backgroundsLoader;

    public Inventory(List<Booster> boosters, BackgroundsLoader backgroundsLoader)
    {
        _backgroundsLoader = backgroundsLoader;
        _boosters = boosters;
    }

    public void AddBooster(Booster booster)
    {
        if (_boosters.Any(item => booster.Type == item.Type))
        {
            booster.AddBooster();
            CountBoostersChanged?.Invoke(booster);
        }
    }

    public void UseBooster(Booster booster)
    {
        if (_boosters.Any(item => booster.Type == item.Type))
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

    public void UseBackground(Background background)
    {
        _backgroundsLoader.SetBackground(background.Type);
    }

    public void LoadSaveData(Booster booster)
    {
        if (booster.HasBooster())
        {
            CountBoostersChanged?.Invoke(booster);
        }
    }
}
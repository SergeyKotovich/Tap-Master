using System;
using System.Collections.Generic;

public interface IInventoryHandler
{
    public event Action<Booster> BoostersDepleted;
    public event Action <Booster> CountBoostersChanged;
    void AddBooster(Booster booster);
    void UseBooster(Booster booster);

}